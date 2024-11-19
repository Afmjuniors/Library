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
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TDCore.DependencyInjection;

namespace NN.Checklist.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : BaseController<ApplicationController>
    {
        /// <summary>
        /// Name: "ApplicationController" 
        /// Description: is a constructor that takes ILogger as a parameter.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public ApplicationController(ILogger<ApplicationController> logger) : base(logger) { }


        /// <summary>
        /// Name: "Download" 
        /// Description: method, if it returns 200OK, the method will download the file specified in the route
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>        

        [HttpGet("Download")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Download([FromQuery] string name)
        {
            try
            {
                var caminho = string.Concat(Configuration.GetSection("ExcelLocation").Value.ToString(), "/", name);

                var memory = new MemoryStream();

                using (var stream = new FileStream(name, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                return File(memory, GetContentType(caminho), name);

            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "GetContentType" 
        /// Description: method gets the content type by the "path" parameter that is passed by the method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }


        #region Country

        /// <summary>
        /// Name: "ListCountries" 
        /// Description: method returns a list of countries.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        [HttpGet("ListCountries")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<CountryDTO>>> ListCountries()
        {
            try
            {
                var user = GetUserFromToken();
                var service = ObjectFactory.GetSingleton<IApplicationService>();
                var status = await service.ListCountries();

                return Ok(status);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }
        #endregion

        #region Processes
        /// <summary>
        /// Name: "ListProcesses" 
        /// Description: method returns a list of processes fetched from "IApplicationService".
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        [HttpGet("ListProcesses")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<ProcessDTO>>> ListProcesses()
        {
            try
            {
                var user = GetUserFromToken();
                var service = ObjectFactory.GetSingleton<IApplicationService>();
                var status = await service.ListProcesses();

                return Ok(status);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }
        #endregion

        #region Areas

        /// <summary>
        /// Name: "InertArea" 
        /// Description: method creates a receiving area as a parameter to the request.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        [HttpPost("InsertArea")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AreaDTO>> InsertArea([FromBody] AreaUpdatedDTO request)
        {
            try
            {
                IApplicationService service = ObjectFactory.GetSingleton<IApplicationService>();
                var user = await GetUserFromToken();
                var area = await service.InsertArea(request.Data, request.Comments, user);

                return Ok(area);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }

        /// <summary>
        /// Name: UpdateArea 
        /// Description: method updates the area that was created.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>        

        [HttpPost("UpdateArea")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AreaDTO>> UpdateArea([FromBody] AreaUpdatedDTO area)
        {
            try
            {
                IApplicationService service = ObjectFactory.GetSingleton<IApplicationService>();
                var user = await GetUserFromToken();

                await service.UpdateArea(user, area.Data, area.Comments);

                return Created(nameof(UpdateArea), area);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "SearchAreas" 
        /// Description: method returns the areas that are in the "pageMessage" parameter
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        [HttpPost("SearchAreas")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AreaPageMessage>> SearchAreas([FromBody] QueryParamsDTO queryParams)
        {
            try
            {

                var service = ObjectFactory.GetSingleton<IApplicationService>();

                AreaPageMessage pageMessage = new AreaPageMessage();
                pageMessage.ActualPage = queryParams.PageNumber;
                pageMessage.PageSize = queryParams.PageSize;

                if (queryParams.Filter != null)
                {
                    AreaDTO filter;
                    filter = JsonConvert.DeserializeObject<AreaDTO>(queryParams.Filter.ToString());

                    pageMessage.AreaId = filter.AreaId;
                    pageMessage.Description = filter.Description;
                    pageMessage.Name = filter.Name;
                    pageMessage.ProcessId = filter.ProcessId;
                }

                var areas = await service.SearchAreas(pageMessage);

                return Ok(areas);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


        /// <summary>
        /// Name: "ListAreas" 
        /// Description: method returns a list of areas being fetched by the "service" variable.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        [HttpGet("ListAreas")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<AreaDTO>>> ListAreas()
        {
            try
            {
                var user = GetUserFromToken();
                var service = ObjectFactory.GetSingleton<IApplicationService>();
                var areas = await service.ListAreas();

                return Ok(areas);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }

        /// <summary>
        /// Name: "ListAreasByProcess" 
        /// Description: method returns a list of processes by Id.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        [HttpGet("ListAreasByProcess/{processId}")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<AreaDTO>>> ListAreasByProcess(long processId)
        {
            try
            {
                var user = await GetUserFromToken();
                var service = ObjectFactory.GetSingleton<IApplicationService>();
                var areas = await service.ListAreasByProcess(processId);

                return Ok(areas);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }

        #endregion
                

        #region AdGroups


        /// <summary>
        /// Name: "ListAdGroups" 
        /// Description: method returns a list of ad groups fetched by the "IApplicationService" service.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>

        [HttpGet("ListAdGroups")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<AdGroupDTO>>> ListAdGroups()
        {
            try
            {
                var user = GetUserFromToken();
                var service = ObjectFactory.GetSingleton<IApplicationService>();
                var adGroups = await service.ListAdGroups();

                return Ok(adGroups);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }
        #endregion
    }
}
