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
    public class BooksController : BaseController<BooksController>
    {
        /// <summary>
        /// Name: AccessControlController 
        /// Description: is a constructor that has a method called authenticate passing as a parameter the user to perform the authentication.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>

        public BooksController(ILogger<BooksController> logger) : base(logger) { }


        /// <summary>
        /// Name: "Authenticate" 
        /// Description: method authenticates the user with the private key by loading the menu.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> CreateBook([FromBody] BookDTO book)
        {
            try
            {
                var _service = ObjectFactory.GetSingleton<IBookService>();

                var privateKey = Configuration.GetSection("Security")["Token"];
                var usuarioLogado = await GetUserFromToken();

                var aut = await _service.CreateBook(usuarioLogado, book);

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
        [HttpPost("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PageMessage<BookDTO>>> SearchBooks([FromBody] BookPageMessage searchParam)
        {
            try
            {
                var _service = ObjectFactory.GetSingleton<IBookService>();

                var privateKey = Configuration.GetSection("Security")["Token"];
                var usuarioLogado = await GetUserFromToken();

                var res = await _service.SearchBooks(usuarioLogado, searchParam);

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
        [HttpGet("User/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<BookDTO>>> ListBooksByUser(long userId)
        {
            try
            {
                var _service = ObjectFactory.GetSingleton<IBookService>();

                var privateKey = Configuration.GetSection("Security")["Token"];
                var usuarioLogado = await GetUserFromToken();

                var res = await _service.ListBooksByUser(usuarioLogado, userId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return CreateError(ex);
            }
        }


    }
}
