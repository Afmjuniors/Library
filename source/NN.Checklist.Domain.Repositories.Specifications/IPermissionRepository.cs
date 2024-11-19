using TDCore.Core;
using TDCore.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 10/2021 12:16:45

#endregion

namespace NN.Checklist.Domain.Repositories.Specifications
{
    [ObjectMap("PermissionRepository", true)]
    public interface IPermissionRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {

        #region User Code
        Task<IList<TEntity>> ListPermissionsByAdGroupId(long adGroupId);

        #endregion

    }
}