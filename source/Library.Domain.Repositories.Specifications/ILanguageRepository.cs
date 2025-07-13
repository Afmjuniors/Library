
using TDCore.Core;
using TDCore.Data;
using TDCore.Data.Paging;
using TDCore.Domain;
using System;
using System.Threading.Tasks;


#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:37

#endregion

namespace Library.Domain.Repositories.Specifications
{
    [ObjectMap("LanguageRepository", true)]
    public interface ILanguageRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {

        #region User Code

        Task<int> GetIdByCode(string lang);

        #endregion

    }
}