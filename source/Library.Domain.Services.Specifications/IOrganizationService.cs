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
    [ObjectMap("OrganizationService", true)]
    public interface IOrganizationService
    {
        Task<List<UserDTO>> ListMembers(AuthenticatedUserDTO user, long organizationId);
        Task<bool> JoinLeaveOrganization(AuthenticatedUserDTO user, long organizationId);
        Task<bool> DeleteOrganization(AuthenticatedUserDTO user, long organizationId);
        Task<OrganizationDTO> GetOrganization(AuthenticatedUserDTO user, long organizationId);
        Task<List<OrganizationDTO>> ListOrganizations(AuthenticatedUserDTO user);
        Task<OrganizationDTO> EditOrganization(AuthenticatedUserDTO user, OrganizationDTO organizationId);
        Task<OrganizationDTO> CreateOrganization(AuthenticatedUserDTO user, OrganizationDTO organizationId);

    }
}
