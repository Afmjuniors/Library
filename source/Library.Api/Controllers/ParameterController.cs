using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Library.Domain.DTO.Response;
using Library.Domain.Services.Specifications;
using System;
using System.Threading.Tasks;
using TDCore.DependencyInjection;

namespace Library.Api.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class ParameterController : BaseController<ParameterController>
    {
        /// <summary>
        /// Name: "ParameterController" 
        /// Description: class constructor.
        /// Created by: [CREATED_BY] 
        /// </summary>        
        public ParameterController(ILogger<ParameterController> logger) : base(logger) { }

        ///// <summary>
        ///// Name: "GetDbParameter" 
        ///// Description: method returns the parameters of the "DBParameterDTO" database.
        ///// Created by: [CREATED_BY] 
        ///// </summary>        
        //[HttpGet("GetDbParameter")]
        //[Authorize()]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<DBParameterDTO>> GetDbParameter()
        //{
        //    try
        //    {
        //        IParameterService _service = ObjectFactory.GetSingleton<IParameterService>();
        //        var user = await GetUserFromToken();
        //        var aut = await _service.GetDBParameter(user);
        //        return Ok(aut);
        //    }
        //    catch (Exception ex)
        //    {
        //        return CreateError(ex);
        //    }
        //}

        ///// <summary>
        ///// Name: "GetDomainParameter" 
        ///// Description: method gets the values of the administrator and the domain.
        ///// Created by: [CREATED_BY] 
        ///// </summary>
        
        //[HttpGet("GetDomainParameter")]
        //[Authorize()]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<DomainParameterDTO>> GetDomainParameter()
        //{
        //    try
        //    {
        //        IParameterService _service = ObjectFactory.GetSingleton<IParameterService>();
        //        var user = await GetUserFromToken();
        //        var aut = await _service.GetDomainParameter(user);
        //        return Ok(aut);
        //    }
        //    catch (Exception ex)
        //    {
        //        return CreateError(ex);
        //    }
        //}
        ///// <summary>
        ///// Name: "GetPolicyParameter" 
        ///// Description: method gets the user's idle time.
        ///// Created by: [CREATED_BY] 
        ///// </summary>

        //[HttpGet("GetPolicyParameter")]
        //[Authorize()]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<PolicyParameterDTO>> GetPolicyParameter()
        //{
        //    try
        //    {
        //        IParameterService _service = ObjectFactory.GetSingleton<IParameterService>();
        //        var user = await GetUserFromToken();
        //        var aut = await _service.GetPolicyParameter(user);
        //        return Ok(aut);
        //    }
        //    catch (Exception ex)
        //    {
        //        return CreateError(ex);
        //    }
        //}
        ///// <summary>
        ///// Name: "GetMailParameter" 
        ///// Description: method gets the email connection parameters.
        ///// Created by: [CREATED_BY] 
        ///// </summary>

        //[HttpGet("GetMailParameter")]
        //[Authorize()]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<MailParameterDTO>> GetMailParameter()
        //{
        //    try
        //    {
        //        IParameterService _service = ObjectFactory.GetSingleton<IParameterService>();
        //        var user = await GetUserFromToken();
        //        var aut = await _service.GetMailParameter(user);
        //        return Ok(aut);
        //    }
        //    catch (Exception ex)
        //    {
        //        return CreateError(ex);
        //    }
        //}


        ///// <summary>
        ///// Name: "InsertMailParameter" 
        ///// Description: method inserts the data of "MailParameterDTO" with a comment.
        ///// Created by: [CREATED_BY] 
        ///// </summary>
        //[HttpPost("InsertMailParameter")]
        //[Authorize()]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<MailParameterDTO>> InsertMailParameter([FromBody] MailParameterUpdatedDTO data)
        //{
        //    try
        //    {
        //        IParameterService _service = ObjectFactory.GetSingleton<IParameterService>();
        //        var user = await GetUserFromToken();
        //        var aut = await _service.InsertMailParameter(data.Data, data.Comments, user);
        //        return Ok(aut);
        //    }
        //    catch (Exception ex)
        //    {
        //        return CreateError(ex);
        //    }
        //}


        ///// <summary>
        ///// Name: "InsertDbParameter"
        ///// Description: method inserts the database connection parameters.
        ///// Created by/: [CREATED_BY] 
        ///// </summary>
        //[HttpPost("InsertDbParameter")]
        //[Authorize()]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<DBParameterDTO>> InsertDbParameter([FromBody] DBParameterUpdatedDTO data)
        //{
        //    try
        //    {
        //        IParameterService _service = ObjectFactory.GetSingleton<IParameterService>();
        //        var user = await GetUserFromToken();
        //        var aut = _service.InsertDBParameter(data.Data, data.Comments, user);
        //        return Ok(aut);
        //    }
        //    catch (Exception ex)
        //    {
        //        return CreateError(ex);
        //    }
        //}

        ///// <summary>
        ///// Name: "InsertDomainParameter" 
        ///// Description: method inserts the values of "DomainParameterDTO" and adds a comment.
        ///// Created by: [CREATED_BY] 
        ///// </summary>
        //[HttpPost("InsertDomainParameter")]
        //[Authorize()]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<DomainParameterDTO>> InsertDomainParameter([FromBody] DomainParameterUpdatedDTO data)
        //{
        //    try
        //    {
        //        IParameterService _service = ObjectFactory.GetSingleton<IParameterService>();
        //        var user = await GetUserFromToken();
        //        var aut = await _service.InsertDomainParameter(data.Data, data.Comments, user);
        //        return Ok(aut);
        //    }
        //    catch (Exception ex)
        //    {
        //        return CreateError(ex);
        //    }
        //}

        ///// <summary>
        ///// Name: "InsertPolicyParameter" 
        ///// Description: method inserts an inactivity policy and adds a comment.
        ///// Created by: [CREATED_BY] 
        ///// </summary>       

        //[HttpPost("InsertPolicyParameter")]
        //[Authorize()]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<PolicyParameterDTO>> InsertPolicyParameter([FromBody] PolicyParameterUpdatedDTO data)
        //{
        //    try
        //    {
        //        IParameterService _service = ObjectFactory.GetSingleton<IParameterService>();
        //        var user = await GetUserFromToken();
        //        var aut = await _service.InsertPolicyParameter(data.Data, data.Comments, user);
        //        return Ok(aut);
        //    }
        //    catch (Exception ex)
        //    {
        //        return CreateError(ex);
        //    }
        //}
    }
}
