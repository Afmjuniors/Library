using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Request;
using NN.Checklist.Domain.DTO.Request.User;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.DTO.Response.User;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TDCore.DependencyInjection;

namespace NN.Checklist.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccessControlController : BaseController<AccessControlController>
    {
        /// <summary>
        /// Name: AccessControlController 
        /// Description: is a constructor that has a method called authenticate passing as a parameter the user to perform the authentication.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>

        public AccessControlController(ILogger<AccessControlController> logger) : base(logger) { }


        /// <summary>
        /// Name: "Authenticate" 
        /// Description: method authenticates the user with the private key by loading the menu.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticatedUserDTO>> Authenticate([FromBody] UserAuthDTO user)
        {
            try
            {
                IAccessControlService _service = ObjectFactory.GetSingleton<IAccessControlService>();

                var privateKey = Configuration.GetSection("Security")["Token"];

                var aut = await _service.Authenticate(user, privateKey);
                if (aut != null)
                {
                    aut.Menu = LoadMenu(aut);
                }
                return Ok(aut);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }

        /// <summary>
        /// Name: GetUserByToken
        /// Description: It's a method that get user by token and if found it would return the logged in user.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>

        [HttpGet("GetUserByToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticatedUserDTO>> GetUserByToken()
        {

            try
            {
                IAccessControlService service = ObjectFactory.GetSingleton<IAccessControlService>();
                var usuarioLogado = await GetUserFromToken();
                
                return usuarioLogado;
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "GetMenu" 
        /// Description: method loads the menu if the user is logged in.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        [HttpGet("GetMenu")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MenuDTO>> GetMenu()
        {
            try
            {
                var usuarioLogado = await GetUserFromToken();
                var menu = LoadMenu(usuarioLogado);
                return menu;
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "LanguageUpdate" 
        /// Description: replaces the previous language with the parameter "LanguageDTO".
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        [HttpPut("UpdateLanguage")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LanguageDTO>> UpdateLanguage([FromBody] LanguageDTO culture)
        {
            var actionUser = await GetUserFromToken();
            try
            {
                var service = ObjectFactory.GetSingleton<IAccessControlService>();

                await service.UpdateUserLanguage(actionUser.UserId, actionUser.UserId, culture.Language);

                return Ok(actionUser);
            }
            catch (Exception ex)
            {
                return BadRequest(CreateError(ex));
            }
        }

        /// <summary>
        /// Name: "SignatureValidation" 
        /// Description: method receives "Signature DTO" as a parameter to validate the signature. In case of failure, an exception will be thrown.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        [HttpPost("SignatureValidation")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SignatureDTO>> SignatureValidation([FromBody] SignatureDTO userDto)
        {
            try
            {
                IAccessControlService _service = ObjectFactory.GetSingleton<IAccessControlService>();
                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
                
                AuthenticatedUserDTO user = await GetUserFromToken();
                string language = user.CultureInfo;

                if (user.UserAD.Trim().ToLower() == userDto.username.Trim().ToLower()) {
                    userDto.cryptData = await _service.SignatureValidate(userDto);
                    userDto.password = "";
                    userDto.username = "";
                    return Ok(userDto);
                }
                else
                {
                    throw new Exception(await globalization.GetString(language, "FailedToSignDiffUser"));
                }
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "ReadSignature" 
        /// Description: receives as a parameter "signature" passing through the "ObjectFactory's" "ReadSignature" method and being decrypted.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        
        [HttpGet("ReadSignature")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SignApprovalDTO>> ReadSignature(string signature)
        {
            try
            {
                IAccessControlService _service = ObjectFactory.GetSingleton<IAccessControlService>();
                AuthenticatedUserDTO user = await GetUserFromToken();
                var aut = await _service.ReadSignature(signature);
                return Ok(aut);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "ListPermissions" 
        /// Description: method returns asy permissions fetched by "IAccessControlService".".
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        [HttpGet("ListPermissions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<PermissionDTO>>> ListPermissions()
        {
            try
            {
                var service = ObjectFactory.GetSingleton<IAccessControlService>();

                var permissions = await service.ListPermissions();

                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "SearchUsers" 
        /// Description: method expects a request to perform a user search, with the "QueryParamsDTO" parameter.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        [HttpPost("SearchUsers")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UsersPageMessage>> SearchUsers([FromBody] QueryParamsDTO queryParams)
        {
            try
            {

                var service = ObjectFactory.GetSingleton<IAccessControlService>();

                UsersPageMessage pageMessage = new UsersPageMessage();
                pageMessage.ActualPage = queryParams.PageNumber;
                pageMessage.PageSize = queryParams.PageSize;

                if (queryParams.Filter != null)
                {
                    UserFilterDTO filter;
                    filter = JsonConvert.DeserializeObject<UserFilterDTO>(queryParams.Filter.ToString());

                    pageMessage.Initials = filter.Initials;
                }

                var users = await service.SearchUsers(pageMessage);

                return Ok(users);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }

        /// <summary>
        /// Name: "InsertAdGroup" 
        /// Description: method makes a post-type request inserting the groups." 
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>        
        
        [HttpPost("InsertAdGroup")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AdGroupDTO>> InsertAdGroup([FromBody] AdGroupUpdatedDTO request)
        {
            try
            {
                IAccessControlService service = ObjectFactory.GetSingleton<IAccessControlService>();
                var user = await GetUserFromToken();
                var obj = request.Data;
                var adGroup = await service.InsertAdGroup(user, obj.Name, obj.Administrator, obj.Permissions, request.Comments);
                
                return Ok(adGroup);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }

        /// <summary>
        /// Name: "RemoveAdGroup" 
        /// Description: method removes the group that was added to the user.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        [HttpPost("RemoveAdGroup")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> RemoveAdGroup([FromBody] AdGroupUpdatedDTO request)
        {
            try
            {
                IAccessControlService service = ObjectFactory.GetSingleton<IAccessControlService>();
                var user = await GetUserFromToken();
                var obj = request.Data;
                var res = await service.RemoveAdGroup(user, obj.AdGroupId, request.Comments);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "SearchAdGroups" 
        /// Description: method waits for a request to perform a group search, "queryParams" is to perform the AdGroups pagination.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        [HttpPost("SearchAdGroups")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AdGroupPageMessage>> SearchAdGroups([FromBody] QueryParamsDTO queryParams)
        {
            try
            {

                var service = ObjectFactory.GetSingleton<IAccessControlService>();

                AdGroupPageMessage pageMessage = new AdGroupPageMessage();
                pageMessage.ActualPage = queryParams.PageNumber;
                pageMessage.PageSize = queryParams.PageSize;

                if (queryParams.Filter != null)
                {
                    AdGroupFilterDTO filter;
                    filter = JsonConvert.DeserializeObject<AdGroupFilterDTO>(queryParams.Filter.ToString());

                    pageMessage.Name = filter.Name;
                }

                var users = await service.SearchAdGroups(pageMessage);

                return Ok(users);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }

        /// <summary>
        /// Name: "ListPhonesNumbersByUser" 
        /// Description: method has its function to search the user's phone list, the search is done by their userId.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        [HttpGet("ListPhonesNumbersByUser/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UserPhoneDTO>>> ListPhonesNumbersByUser(long userId)
        {
            try
            {
                IAccessControlService service = ObjectFactory.GetSingleton<IAccessControlService>();
                var usuarioLogado = await GetUserFromToken();
                var userRet = await service.ListPhonesNumbersByUser(userId);

                return userRet;
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "InsertUserPhone" 
        /// Description: method inserts a phone number to the user.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>

        [HttpPost("InsertUserPhone")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserPhoneDTO>> InsertUserPhone([FromBody] UserPhoneDTO userPhone)
        {
            try
            {
                IAccessControlService service = ObjectFactory.GetSingleton<IAccessControlService>();
                var user = await GetUserFromToken();
                var aut = await service.InsertUserPhone(user, userPhone.CountryId, userPhone.Number, userPhone.UserId);
                return Ok(aut);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }

        /// <summary>
        /// Name: "RemoveUserPhone" 
        /// Description: method removes the user's phone by the user and userPhoneId parameters
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        [HttpPost("RemoveUserPhone")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> RemoveUserPhone([FromBody] UserPhoneDTO userPhone)
        {
            try
            {
                IAccessControlService service = ObjectFactory.GetSingleton<IAccessControlService>();
                var user = await GetUserFromToken();
                var aut = await service.RemoveUserPhone(user, userPhone.UserPhoneId);
                return Ok(aut);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "ListAdGroupsByUser" 
        /// Description: method returns a list of Groups by user ID.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        [HttpGet("ListAdGroupsByUser/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AdGroupUserDTO>>> ListAdGroupsByUser(long userId)
        {
            try
            {
                IAccessControlService service = ObjectFactory.GetSingleton<IAccessControlService>();
                var usuarioLogado = await GetUserFromToken();
                var adGroups = await service.ListAdGroupsByUser(userId);

                return adGroups;
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "ActivateUser" 
        /// Description: method makes a request to activate the user passing "UserDTO" as parameter.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        [HttpPost("ActivateUser")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> ActivateUser([FromBody] UserDTO user)
        {
            try
            {
                IAccessControlService service = ObjectFactory.GetSingleton<IAccessControlService>();
                var loggedUser = await GetUserFromToken();
                await service.ActivateUser(loggedUser, user.UserId, user.Active);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "GetAdGroup" 
        /// Description: method searches the ad group by Id.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        [HttpGet("GetAdGroup/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AdGroupDTO>> GetAdGroup(long id)
        {
            try
            {
                var service = ObjectFactory.GetSingleton<IAccessControlService>();
                var adGroup = await service.GetAdGroupById(id);

                return Ok(adGroup);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "UpdateAdGroup" 
        /// Description: method updates the ad group when a request is made.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        [HttpPost("UpdateAdGroup")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AdGroupDTO>> UpdateAdGroup([FromBody] AdGroupUpdatedDTO request)
        {
            try
            {
                var service = ObjectFactory.GetSingleton<IAccessControlService>(); 
                var user = await GetUserFromToken();
                var obj = request.Data;

                await service.UpdateAdGroup(user, obj, request.Comments);

                return Created(nameof(UpdateAdGroup), obj);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }
    }
}
