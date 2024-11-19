using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Domain;

#region Cabeçalho
            
//TECHDRIVEN DIGITAL
//Generated: 10/2021 12:16:43

#endregion

namespace NN.Checklist.Domain.Repositories.Specifications
{
    [ObjectMap("ProcessRepository", true)]
    public interface IProcessRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {

        #region User Code

        Task<TEntity> GetByAcronym(string acronym);

    #endregion

    }
}