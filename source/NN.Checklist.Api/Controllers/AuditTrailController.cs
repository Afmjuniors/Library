using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Request;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TDCore.DependencyInjection;

namespace NN.Checklist.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuditTrailController :  BaseController<AuditTrailController>
    {
        /// <summary>
        /// Name: "AuditTrailController" 
        /// Description: is a constructor that takes logger as a parameter.
        /// Created by: [CREATED_BY] 
        /// </summary>
        public AuditTrailController(ILogger<AuditTrailController> logger) : base(logger) { }


        /// <summary>
        /// Name: "Search" 
        /// Description: method filters the AuditTrail by passing the "AuthenticatedUserDTO" and "SystemRecordPageMessage" parameters.
        /// Created by: [CREATED_BY] 
        /// </summary>
        [HttpPost("Search")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SystemRecordPageMessage>> Search([FromBody] QueryParamsDTO queryParams)
        {
            try
            {
                IAuditTrailService _service = ObjectFactory.GetSingleton<IAuditTrailService>();

                SystemRecordPageMessage pageMessage = new SystemRecordPageMessage();
                pageMessage.ActualPage = queryParams.PageNumber;
                pageMessage.PageSize = queryParams.PageSize;

                var user = await GetUserFromToken();
                if (queryParams.Filter != null)
                {
                    SearchParamsAuditTrailDTO filter;
                    filter = JsonConvert.DeserializeObject<SearchParamsAuditTrailDTO>(queryParams.Filter.ToString());

                    pageMessage.Functionality = filter.Functionality;
                    pageMessage.Keyword = filter.Keyword;
                    if (filter.StartDate.HasValue)
                    {
                        pageMessage.StartDate = filter.StartDate.Value.ToLocalTime();
                    }
                    if (filter.EndDate.HasValue)
                    {
                        pageMessage.EndDate = filter.EndDate.Value.ToLocalTime();
                    }                    
                    pageMessage.UserId = filter.UserId;
                }

                var aut = await _service.Search(user, pageMessage);

                return Ok(aut);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "ListSystemsFunctionalities" 
        /// Description: method returns a list of system functionalities by the "IAuditTrailService" service.
        /// Created by: [CREATED_BY] 
        /// </summary>

        [HttpGet("ListSystemsFunctionalities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<SystemFunctionalityDTO>>> ListSystemFunctionalities()
        {
            try
            {
                var service = ObjectFactory.GetSingleton<IAuditTrailService>();

                var functionalities = await service.ListSystemFunctionalities();

                return Ok(functionalities);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }
    }
}
