
using NN.Checklist.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Domain;

#region Cabeçalho
            
//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:10

#endregion

namespace NN.Checklist.Domain.Repositories.Specifications
{
    [ObjectMap("ItemChecklistRepository", true)]
    public interface IItemChecklistRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {

        #region User Code

        Task<IList<TEntity>> ListAllItensByChecklistIdAndIdTemplate(long checklistId, long itemTemplateId);

    #endregion

    }
}