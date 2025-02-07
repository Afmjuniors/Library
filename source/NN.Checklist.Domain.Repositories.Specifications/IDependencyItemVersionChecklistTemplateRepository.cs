
using System.Collections.Generic;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Domain;

#region Cabeçalho
            
//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:12

#endregion

namespace NN.Checklist.Domain.Repositories.Specifications
{
    [ObjectMap("DependencyItemVersionChecklistTemplateRepository", true)]
    public interface IDependencyItemVersionChecklistTemplateRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {

        #region User Code

        Task<IList<TEntity>> ListAllBLocksFromDependentBlockIdOrItemBlockId(long? blockId, long? itemId);

    #endregion

    }
}