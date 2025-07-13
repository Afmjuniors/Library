using Library.Domain.DTO;
using Library.Domain.DTO.Paging;
using Library.Domain.DTO.Response;
using Library.Domain.Services.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TDCore.Data.Paging;
using TDCore.DependencyInjection;

namespace Library.Api.Controllers
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


        #region BOOKS

        /// <summary>
        /// Name: "SearchAreas" 
        /// Description: method returns the areas that are in the "pageMessage" parameter
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        [HttpPost("Books/Search")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PageMessage<BookDTO>>> SearchBooks([FromBody] BookPageMessage queryParams)
        {
            try
            {

                var service = ObjectFactory.GetSingleton<IApplicationService>();

                //var areas = await service.SearchAreas(pageMessage);

                return Ok();
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }

        /// <summary>
        /// Name: "GetBook" 
        /// Description: method returns the areas that are in the "pageMessage" parameter
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        [HttpGet("Books")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<BookDTO>> GetBook([FromQuery] long bookId)
        {
            try
            {

                var service = ObjectFactory.GetSingleton<IApplicationService>();



                //var areas = await service.GetBook(bookId);

                return Ok();
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }
        /// </summary>
        [HttpPost("Books")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<BookDTO>> SaveBook([FromBody] BookDTO bookId)
        {
            try
            {

                var service = ObjectFactory.GetSingleton<IApplicationService>();



                //var areas = await service.SaveBook(bookId);

                return Ok();
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }
        [HttpDelete("Books")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<bool>> DeleteBook([FromQuery] long bookId)
        {
            try
            {

                var service = ObjectFactory.GetSingleton<IApplicationService>();



                //var areas = await service.DeleteBook(bookId);

                return Ok();
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }

        #endregion
    }
}
