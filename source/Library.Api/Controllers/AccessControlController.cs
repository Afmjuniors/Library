using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Library.Domain.DTO;
using Library.Domain.DTO.Paging;
using Library.Domain.DTO.Request;
using Library.Domain.DTO.Response;
using Library.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TDCore.DependencyInjection;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
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

                return Ok(aut);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }
        /// <summary>
        /// Name: "Authenticate" 
        /// Description: method authenticates the user with the private key by loading the menu.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        [HttpPost("CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticatedUserDTO>> CreateUser([FromBody] UserDTO user)
        {
            try
            {
                IAccessControlService _service = ObjectFactory.GetSingleton<IAccessControlService>();

                var privateKey = Configuration.GetSection("Security")["Token"];

                var aut = await _service.CreateUser(user);

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



        ///// <summary>
        ///// Name: "LanguageUpdate" 
        ///// Description: replaces the previous language with the parameter "LanguageDTO".
        ///// Created by: wazc Programa Novo 2022-09-08 
        ///// </summary>
        //[HttpPut("UpdateLanguage")]
        //[Authorize()]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //public async Task<ActionResult<LanguageDTO>> UpdateLanguage([FromBody] LanguageDTO culture)
        //{
        //    var actionUser = await GetUserFromToken();
        //    try
        //    {
        //        var service = ObjectFactory.GetSingleton<IAccessControlService>();

        //        await service.UpdateUserLanguage(actionUser.UserId, actionUser.UserId, culture.Language);

        //        return Ok(actionUser);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(CreateError(ex));
        //    }
        //}



    }
}
