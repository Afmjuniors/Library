using Library.Domain.DTO;
using Library.Domain.DTO.Paging;
using Library.Domain.DTO.Request;
using Library.Domain.DTO.Response;
using Library.Domain.Services.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TDCore.Data.Paging;
using TDCore.DependencyInjection;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrganizationsController : BaseController<OrganizationsController>
    {
        /// <summary>
        /// Name: AccessControlController 
        /// Description: is a constructor that has a method called authenticate passing as a parameter the user to perform the authentication.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>

        public OrganizationsController(ILogger<OrganizationsController> logger) : base(logger) { }



        /// <summary>
        /// Name: "Authenticate" 
        /// Description: method authenticates the user with the private key by loading the menu.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        [HttpGet("{id}/members")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UserDTO>>> ListMembers([FromRoute] long id)
        {
            try
            {
                var _service = ObjectFactory.GetSingleton<IOrganizationService>();

                var privateKey = Configuration.GetSection("Security")["Token"];
                var usuarioLogado = await GetUserFromToken();

                var res = await _service.ListMembers(usuarioLogado, id);

                return Ok(res);
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
        [HttpGet("{organizationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrganizationDTO>> GetOrganization(long organizationId)
        {
            try
            {
                var _service = ObjectFactory.GetSingleton<IOrganizationService>();

                var privateKey = Configuration.GetSection("Security")["Token"];
                var usuarioLogado = await GetUserFromToken();

                var aut = await _service.GetOrganization(usuarioLogado, organizationId);

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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<OrganizationDTO>>> ListOrganizations()
        {
            try
            {
                var _service = ObjectFactory.GetSingleton<IOrganizationService>();

                var privateKey = Configuration.GetSection("Security")["Token"];
                var usuarioLogado = await GetUserFromToken();

                var aut = await _service.ListOrganizations(usuarioLogado);

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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrganizationDTO>> CreateOrganization([FromBody] OrganizationDTO organizationDTO)
        {
            try
            {
                var _service = ObjectFactory.GetSingleton<IOrganizationService>();

                var privateKey = Configuration.GetSection("Security")["Token"];
                var usuarioLogado = await GetUserFromToken();

                var aut = await _service.CreateOrganization(usuarioLogado, organizationDTO);

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
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrganizationDTO>> EditOrganization([FromBody] OrganizationDTO organizationDTO)
        {
            try
            {
                var _service = ObjectFactory.GetSingleton<IOrganizationService>();

                var privateKey = Configuration.GetSection("Security")["Token"];
                var usuarioLogado = await GetUserFromToken();

                var aut = await _service.EditOrganization(usuarioLogado, organizationDTO);

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
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteOrganization([FromRoute] long id)
        {
            try
            {
                var _service = ObjectFactory.GetSingleton<IOrganizationService>();

                var privateKey = Configuration.GetSection("Security")["Token"];
                var usuarioLogado = await GetUserFromToken();

                var res = await _service.DeleteOrganization(usuarioLogado, id);

                return Ok(res);
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
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> JoinLeaveOrganization([FromRoute] long id)
        {
            try
            {
                var _service = ObjectFactory.GetSingleton<IOrganizationService>();

                var privateKey = Configuration.GetSection("Security")["Token"];
                var usuarioLogado = await GetUserFromToken();

                var res = await _service.JoinLeaveOrganization(usuarioLogado, id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }




    }
}
