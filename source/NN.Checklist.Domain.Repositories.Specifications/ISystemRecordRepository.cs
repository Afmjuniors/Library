
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Paging;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Data.Paging;
using TDCore.Domain;


#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 10/2021 15:59:05

#endregion

namespace NN.Checklist.Domain.Repositories.Specifications
{
    [ObjectMap("SystemRecordRepository", true)]
    public interface ISystemRecordRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {

        #region User Code

        Task<PageMessage<SystemRecordDTO>> Search(SystemRecordPageMessage data);

    #endregion

    }
}