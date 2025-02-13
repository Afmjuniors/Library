using TDCore.Core;
using TDCore.Data.Paging;
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Common;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Request;
using NN.Checklist.Domain.DTO.Request.User;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.DTO.Response.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.Services.Specifications
{
    [ObjectMap("AccessControlService", true)]
    public interface IAccessControlService
    {
        Task<AuthenticatedUserDTO> Authenticate(UserAuthDTO user, string privateKey);
        Task<AuthenticatedUserDTO> AuthenticateAdmin(UserAuthDTO user, string privateKey);
        Task<AuthenticatedUserDTO> GetUser(long idUser);
        Task<string> SignatureValidate(SignatureDTO user);
        Task<SignApprovalDTO> ReadSignature(string value);
        Task<List<PermissionDTO>> ListPermissions();
        Task<List<AdGroupUserDTO>> ListAdGroupsByUser(long userId);
        Task<PageMessage<UserDTO>> SearchUsers(UsersPageMessage data);        
        Task ActivateUser(AuthenticatedUserDTO user, long userId, bool active);
        Task<AdGroupDTO> InsertAdGroup(AuthenticatedUserDTO user, string name, bool administrator, List<PermissionDTO> permissions, string comments);
        Task<bool> RemoveAdGroup(AuthenticatedUserDTO user, long adGroupId, string comments);
        Task UpdateAdGroup(AuthenticatedUserDTO user, AdGroupDTO adGroup, string comments);
        Task<PageMessage<AdGroupDTO>> SearchAdGroups(AdGroupPageMessage data);
        Task<AdGroupDTO> GetAdGroupById(long adGroupId);
        Task UpdateUserLanguage(long actionUserId, long userId, string language);
    }
}
