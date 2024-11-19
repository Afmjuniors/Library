using TDCore.Core;
using TDCore.Domain;
using NN.Checklist.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.Repositories.Specifications
{
    [ObjectMap("AdGroupUserRepository", true)]
    public interface IAdGroupUserRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        Task<IList<TEntity>> ListAdGroupUsersByIdUser(long idUser);
        Task<bool> ExistsByGroup(long adGroupId);
    }
}
