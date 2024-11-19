
using TDCore.Core;
using TDCore.Data;
using TDCore.Data.Paging;
using TDCore.Domain;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Response.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:37

#endregion

namespace NN.Checklist.Domain.Repositories.Specifications
{
    [ObjectMap("UserRepository", true)]
    public interface IUserRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {

        #region User Code
        Task<TEntity> GetByInitials(string initials);
        Task<IList<TEntity>> ListByAreaAvailability(long idArea);
        Task<IList<TEntity>> ListMains(long idArea);
        Task<IList<TEntity>> ListQAByAreaAvailability(long idArea);
        Task<IList<TEntity>> ListQAs(long? idArea);
        Task<IList<TEntity>> ListImpactAnalystByAreaAvailability(long idArea);
        Task<IList<TEntity>> ListImpactsAnalysts(long idArea);
        Task<IList<TEntity>> ListByAreaOccurrence(long idArea, long idOccurrence, bool? includeQa);
        Task<IList<TEntity>> ListByAdministratorGroup();
        Task<IList<TEntity>> ListAdministrators();
        Task<PageMessage<UserDTO>> Search(UsersPageMessage data);
    #endregion
    }
}