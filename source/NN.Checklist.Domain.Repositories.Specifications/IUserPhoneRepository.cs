
using TDCore.Core;
using TDCore.Data;
using TDCore.Data.Paging;
using TDCore.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:38

#endregion

namespace NN.Checklist.Domain.Repositories.Specifications
{
    [ObjectMap("UserPhoneRepository", true)]
    public interface IUserPhoneRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {

        #region User Code

        Task<IList<TEntity>> ListByUser(long userId);

    #endregion

    }
}