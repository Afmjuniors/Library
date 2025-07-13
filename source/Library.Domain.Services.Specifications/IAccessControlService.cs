using TDCore.Core;
using TDCore.Data.Paging;
using Library.Domain.DTO;
using Library.Domain.DTO.Common;
using Library.Domain.DTO.Paging;
using Library.Domain.DTO.Request;
using Library.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Domain.Services.Specifications
{
    [ObjectMap("AccessControlService", true)]
    public interface IAccessControlService
    {
        Task<AuthenticatedUserDTO> Authenticate(UserAuthDTO user, string privateKey);
        Task<AuthenticatedUserDTO> AuthenticateAdmin(UserAuthDTO user, string privateKey);
        Task<AuthenticatedUserDTO> GetUser(long idUser);
        //Task<PageMessage<UserDTO>> SearchUsers(UsersPageMessage data);
        Task UpdateUserLanguage(long actionUserId, long userId, string language);
        Task<AuthenticatedUserDTO> CreateUser(UserDTO user);
    }
}
