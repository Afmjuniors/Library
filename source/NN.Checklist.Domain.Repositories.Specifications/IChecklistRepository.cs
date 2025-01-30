
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Data.Paging;
using TDCore.Domain;
using System.Collections.Generic;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:12

#endregion

namespace NN.Checklist.Domain.Repositories.Specifications
{
    [ObjectMap("ChecklistRepository", true)]
    public interface IChecklistRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {

        #region User Code

        Task<PageMessage<ChecklistDTO>> Search(ChecklistPageMessage data);

        Task<IList<TEntity>> ListChecklistByVersion(long versionChecklistTemplateId);

        #endregion

    }
}