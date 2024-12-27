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
        /// Created by: [CREATED_BY] 
        /// </summary>
        public ApplicationController(ILogger<ApplicationController> logger) : base(logger) { }


        /// <summary>
        /// Name: "Download" 
        /// Description: method, if it returns 200OK, the method will download the file specified in the route
        /// Created by: [CREATED_BY] 
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
        /// Created by: [CREATED_BY] 
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
        /// Created by: [CREATED_BY] 
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
     

        #region AdGroups


        /// <summary>
        /// Name: "ListAdGroups" 
        /// Description: method returns a list of ad groups fetched by the "IApplicationService" service.
        /// Created by: [CREATED_BY] .
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
