using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Services.Specifications;
using NPOI.OpenXmlFormats.Dml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TDCore.DependencyInjection;

namespace NN.Checklist.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChecklistController : BaseController<ChecklistController>
    {

        /// <summary>
        /// Name: "ApplicationController" 
        /// Description: is a constructor that takes ILogger as a parameter.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public ChecklistController(ILogger<ChecklistController> logger) : base(logger) { }


        /// <summary>
        /// Name: "Download" 
        /// Description: method, if it returns 200OK, the method will download the file specified in the route
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>        

        [HttpGet("GetChecklist")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetCheckList()
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
        /// Name: "Download" 
        /// Description: method, if it returns 200OK, the method will download the file specified in the route
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>        

        [HttpGet("GetChecklistVersions")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetChecklistVersions([FromQuery] long checklist)
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
        /// Name: "Download" 
        /// Description: method, if it returns 200OK, the method will download the file specified in the route
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>        

        [HttpGet("GetChecklistVersionTemplate")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetChecklistVersionTemplate([FromQuery] long versionId)
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


    }
}