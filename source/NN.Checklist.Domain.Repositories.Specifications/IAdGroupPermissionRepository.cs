using TDCore.Core;
using TDCore.Domain;
using NN.Checklist.Domain.DTO.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 10/2021 12:16:45

#endregion

namespace NN.Checklist.Domain.Repositories.Specifications
{
    [ObjectMap("AdGroupPermissionRepository", true)]
    public interface IAdGroupPermissionRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {

        #region User Code

        Task<IList<AdGroupPermissionDTO>> ListAllGroupPermissions();
        Task<IList<TEntity>> ListAdGroupPermissionsByIdAdGroup(long adGroupId);

        #endregion

    }
}