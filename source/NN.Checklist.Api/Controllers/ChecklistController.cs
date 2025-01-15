using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Request;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Services.Specifications;
using NPOI.OpenXmlFormats.Dml;
using NPOI.POIFS.Crypt.Dsig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TDCore.Data.Paging;
using TDCore.DependencyInjection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NN.Checklist.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChecklistController : BaseController<ChecklistController>
    {

        /// <summary>
        /// Name: "ApplicationController" 
        /// Description: is a constructor that takes ILogger as a parameter.
        /// Created by:[CREATED_BY]
        /// </summary>
        public ChecklistController(ILogger<ChecklistController> logger) : base(logger) { }


        /// <summary>
        /// Name: "GetCheckList" 
        /// Description: method,to get all checklists base
        /// Created by: [CREATED_BY] 
        /// </summary>        

        [HttpGet("ListChecklist")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ListChecklist()
        {
            try
            {
                IChecklistService service = ObjectFactory.GetSingleton<IChecklistService>();

                var result = service.ListChecklist();

                return Ok(result);

            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }

        /// <summary>
        /// Name: "GetChecklistVersions" 
        /// Description: method, to get all versions from a checklist base
        /// Created by: [CREATED_BY] 
        /// </summary>        

        [HttpGet("GetChecklistVersions")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetChecklistVersions([FromQuery] long checklist)
        {
            try
            {
                IChecklistService service = ObjectFactory.GetSingleton<IChecklistService>();

                var result = service.GetLatestCheckList(checklist);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }

        /// <summary>
        /// Name: "GetChecklistVersionTemplate" 
        /// Description: method,to get the template from a verion, case version is null , as default the latest cerion is selected
        /// Created by: [CREATED_BY] 
        /// </summary>        

        [HttpGet("ListChecklistVersions")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ListChecklistVersions([FromQuery] long checklistTemplateId)
        {
            try
            {
                IChecklistService service = ObjectFactory.GetSingleton<IChecklistService>();

                var result = service.ListChecklistVersions(checklistTemplateId);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }
        /// <summary>
        /// Name: "CreateNewChecklist" 
        /// Description: method,to create an new checklist by the user
        /// Created by: [CREATED_BY] 
        /// </summary>        

        [HttpPost("CreateNewChecklist")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateNewChecklist([FromBody] object data)
        {
            try
            {
                return Ok();

            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }
        /// <summary>
        /// Name: "NewChecklist" 
        /// Description: method,to create a new black checklist form an template 
        /// Created by: [CREATED_BY] 
        /// </summary>        

        [HttpPost("NewChecklist")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> NewChecklist([FromBody] ChecklistRequestDTO data)
        {
            try
            {
                var user = await GetUserFromToken();

                IChecklistService service = ObjectFactory.GetSingleton<IChecklistService>();
               var result = await service.NewUpdateChecklist(user,data.Data);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }

        /// <summary>
        /// Name: "CreateNewChecklistVersion" 
        /// Description: method,to create a new version of a checklist 
        /// Created by: [CREATED_BY] 
        /// </summary>        

        [HttpPost("CreateNewChecklistVersion")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateNewChecklistVersion([FromBody] object data)
        {
            try
            {
                return Ok();

            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "UpdateCheckList" 
        /// Description: method,to update a checklist that already initialized 
        /// Created by: [CREATED_BY] 
        /// </summary>        

        [HttpPost("UpdateCheckList")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateCheckList([FromBody] object data)
        {
            try
            {
                return Ok();

            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }

        /// <summary>
        /// Name: "RemoveCheckList" 
        /// Description: method,to remove a checklist
        /// Created by: [CREATED_BY] 
        /// </summary>        

        [HttpPost("RemoveCheckList")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RemoveCheckList([FromBody] object data)
        {
            try
            {
                return Ok();

            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }
        /// <summary>
        /// Name: "SearchChecklists" 
        /// Description: method,to search all checklists pagged
        /// Created by: [CREATED_BY] 
        /// </summary>        

        [HttpPost("SearchChecklists")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ChecklistDTO>> SearchChecklists([FromBody] QueryParamsDTO queryParams)
        {
            try
            {
                var user = await GetUserFromToken();

                ChecklistPageMessage pageMessage = new ChecklistPageMessage();
                pageMessage.ActualPage = queryParams.PageNumber;
                pageMessage.PageSize = queryParams.PageSize>0? queryParams.PageSize:10;
                SearchChecklistParamsDTO filter;
                filter = JsonConvert.DeserializeObject<SearchChecklistParamsDTO>(queryParams.Filter.ToString());


                if (filter.StartDate.HasValue)
                {
                    pageMessage.StartDate = filter.StartDate.Value.ToLocalTime();
                }
                if (filter.EndDate.HasValue)
                {
                    pageMessage.EndDate = filter.EndDate.Value.ToLocalTime();
                }
                if (filter.ChecklistId.HasValue)
                {
                    pageMessage.ChecklistId = filter.ChecklistId.Value;
                }
                if (filter.ChecklistTemplateId.HasValue)
                {
                    pageMessage.ChecklistTemplateId = filter.ChecklistTemplateId.Value;
                }
                if (filter.VersionChecklistTemplateId.HasValue)
                {
                    pageMessage.VersionChecklistTemplateId = filter.VersionChecklistTemplateId.Value;
                }

        IChecklistService service = ObjectFactory.GetSingleton<IChecklistService>();
                var result = await service.Search(user, pageMessage);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }
        /// <summary>
        /// Name: "SearchChecklists" 
        /// Description: method,to search all checklists pagged
        /// Created by: [CREATED_BY] 
        /// </summary>        

        [HttpPost("SignItemChecklist")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemChecklistDTO>> SignItemChecklist([FromBody] SignatureItemDTO data)
        {
            try
            {
                var user = await GetUserFromToken();


                IChecklistService service = ObjectFactory.GetSingleton<IChecklistService>();
                var result = await service.SignItem(user, data.Data);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }

        /// <summary>
        /// Name: "ListSignature" 
        /// Description: receives as a parameter "signature" passing through the "ObjectFactory's" "ReadSignature" method and being decrypted.
        /// Created by:[CREATED_BY] 
        /// </summary>

        [HttpGet("ListSignature")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SignApprovalDTO>> ListSignature([FromQuery] long checklistId, long itemTemplateId)
        {
            try
            {
                IChecklistService _service = ObjectFactory.GetSingleton<IChecklistService>();
                AuthenticatedUserDTO user = await GetUserFromToken();
                var result = await _service.ListAllSignuture(user,checklistId,itemTemplateId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }




    }
}