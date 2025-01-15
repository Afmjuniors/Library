
using NN.Checklist.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Domain;

#region Cabeçalho
            
//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:11

#endregion

namespace NN.Checklist.Domain.Repositories.Specifications
{
    [ObjectMap("VersionChecklistTemplateRepository", true)]
    public interface IVersionChecklistTemplateRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {

        #region User Code

       public Task<TEntity> GetLatestVersionFromChecklistId(long checklistId);
        Task<IList<TEntity>> ListVersionFromChecklistTemplateId(long checklistId);

    #endregion

    }
}