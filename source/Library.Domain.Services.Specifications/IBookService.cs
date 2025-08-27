using TDCore.Core;
using TDCore.Data.Paging;
using Library.Domain.DTO;
using Library.Domain.DTO.Common;
using Library.Domain.DTO.Paging;
using Library.Domain.DTO.Request;
using Library.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Domain.Services.Specifications
{
    [ObjectMap("BookService", true)]
    public interface IBookService
    {
        Task<bool> CreateBook(AuthenticatedUserDTO user, BookDTO book);
        Task<PageMessage<BookDTO>> SearchBooks(AuthenticatedUserDTO user, BookPageMessage searchParams);
        Task<List<BookDTO>> ListBooksByUser(AuthenticatedUserDTO user, long userId);

    }
}
