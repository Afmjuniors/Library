
using Library.Domain.DTO;
using Library.Domain.DTO.Paging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Data;
using TDCore.Data.Paging;
using TDCore.Domain;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:37

#endregion

namespace Library.Domain.Repositories.Specifications
{
    [ObjectMap("BookRepository", true)]
    public interface IBookRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {

        #region User Code
        Task<PageMessage<BookDTO>> Search(BookPageMessage queryParams);

    #endregion
    }
}