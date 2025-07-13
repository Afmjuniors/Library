
using TDCore.Core;
using TDCore.Data;
using TDCore.Data.Paging;
using TDCore.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:37

#endregion

namespace Library.Domain.Repositories.Specifications
{
    [ObjectMap("UserRepository", true)]
    public interface IUserRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {

        Task<TEntity> GetUserByEmail(string email);
    }
}