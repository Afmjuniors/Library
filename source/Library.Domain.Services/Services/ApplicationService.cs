using Library.Domain.Common;
using Library.Domain.DTO;
using Library.Domain.DTO.Paging;
using Library.Domain.DTO.Response;
using Library.Domain.Entities;
using Library.Domain.Services.Specifications;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Data.Paging;
using TDCore.DependencyInjection;

namespace Library.Domain.Services
{
    public class ApplicationService : ObjectBase, IApplicationService
    {
        #region Country
        /// <summary>
        /// Name: "ListCountries" 
        /// Description: method gets a list of countries.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<List<CountryDTO>> ListCountries()
        {
            try
            {
                var list = await Country.Repository.ListAll();
                if (list != null && list.Count > 0)
                {
                    return list.TransformList<CountryDTO>().ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region User

        #endregion

        #region Books

        public async Task<bool> InsertBook(AuthenticatedUserDTO user, BookDTO book)
        {
            try
            {

                new Book(user, book.Name, book.Description,book.Author, book.Genre, book.Url, book.Image);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("ErroInseringBook", ex);
            }
        }
        public async Task<bool> UpdateBook(AuthenticatedUserDTO user, BookDTO book)
        {
            try
            {
                var bookToUpdate = await Book.Repository.Get(book.BookId);

                await bookToUpdate.Update(book.Name,book.Genre,book.Author,book.Description,book.BookStatusId,book.Url,book.Image);


                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("ErroInseringBook", ex);
            }
        }

        public async Task<bool> DeleteBook(AuthenticatedUserDTO user, long bookId)
        {
            try
            {
                var bookToDelete = await Book.Repository.Get(bookId);
                if (bookToDelete != null)
                {
                    await bookToDelete.Delete();
                }

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("ErroInseringBook", ex);
            }
        }
        public async Task<BookDTO> GetBook(AuthenticatedUserDTO user, long bookId)
        {
            try
            {
                var book = await Book.Repository.Get(bookId);
                if (book != null)
                {

                    return book.Transform<BookDTO>();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw new Exception("ErroInseringBook", ex);
            }
        }



        public async Task<PageMessage<BookDTO>> SearchBooks(AuthenticatedUserDTO user, BookPageMessage queryParams)
        {
            try
            {
                var books = await Book.Repository.Search(queryParams);
                return books;

            }
            catch (Exception ex)
            {

                throw new Exception("ErroInseringBook", ex);
            }
        }
        #endregion


    }
}
