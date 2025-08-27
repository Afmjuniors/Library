using Library.Domain.DTO;
using Library.Domain.DTO.Paging;
using Library.Domain.DTO.Request;
using Library.Domain.DTO.Response;
using Library.Domain.Entities;
using Library.Domain.Services.Specifications;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Core.Logging;
using TDCore.Data.Paging;
using TDCore.DependencyInjection;
using TDCore.Domain.Exceptions;
using TDCore.Web.Security;
using TDCore.Windows.ActiveDirectory;

namespace Library.Domain.Services
{
    public class BookService : ObjectBase, IBookService
    {
        protected readonly ILog _logger = ObjectFactory.GetSingleton<ILog>();

        public async Task<bool> CreateBook(AuthenticatedUserDTO user, BookDTO bookDto)
        {
            try
            {

                var book = new Book(user, bookDto.Name, bookDto.Description, bookDto.Author, bookDto.Genre, bookDto.Url, bookDto.Image);

                return book != null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<PageMessage<BookDTO>> SearchBooks(AuthenticatedUserDTO user, BookPageMessage searchParams)
        {
            try
            {
                if(searchParams.Filter == null)
                {
                    var newFilter = new SearchBookParamsDTO();
                    searchParams.Filter = newFilter;
                }

                var books = await Book.Repository.Search(searchParams);

                return books;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BookDTO>> ListBooksByUser(AuthenticatedUserDTO user, long userId)
        {
            try
            {
                var books = await Book.Repository.ListBooksByUser(userId);

                if (books != null)
                {
                    return books.ToList();

                }
                return default;

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
