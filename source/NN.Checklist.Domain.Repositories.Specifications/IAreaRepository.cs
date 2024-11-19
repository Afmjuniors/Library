using TDCore.Core;
using TDCore.Data.Paging;
using TDCore.Domain;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Request.Batch;
using NN.Checklist.Domain.DTO.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 10/2021 12:16:42

#endregion

namespace NN.Checklist.Domain.Repositories.Specifications
{
    [ObjectMap("AreaRepository", true)]
    public interface IAreaRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {

        #region User Code

        Task<TEntity> GetByName(long processId, string name);
        Task<PageMessage<AreaDTO>> Search(AreaPageMessage data);

        Task<IList<TEntity>> ListByProcess(long processId);

    #endregion

    }
}