using TDCore.Core;
using TDCore.Data.Paging;
using TDCore.Domain;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 10/2021 12:16:44

#endregion

namespace NN.Checklist.Domain.Repositories.Specifications
{
    [ObjectMap("AdGroupRepository", true)]
    public interface IAdGroupRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {

        #region User Code
        Task<TEntity> GetByAdGroupName(string name);
        Task<PageMessage<AdGroupDTO>> Search(AdGroupPageMessage data);


        #endregion

    }
}