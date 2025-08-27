using Library.Domain.DTO;
using Library.Domain.DTO.Paging;
using Library.Domain.DTO.Request;
using Library.Domain.DTO.Response;
using Library.Domain.Entities;
using Library.Domain.Services.Specifications;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Core.Logging;
using TDCore.Data.Paging;
using TDCore.DependencyInjection;
using TDCore.Domain.Exceptions;
using TDCore.Web.Security;
using TDCore.Windows.ActiveDirectory;

namespace Library.Domain.Services
{
    public class OrganizationService : ObjectBase, IOrganizationService
    {
        protected readonly ILog _logger = ObjectFactory.GetSingleton<ILog>();

        public async Task<List<UserDTO>> ListMembers(AuthenticatedUserDTO user, long organizationId)
        {
            try
            {
                var members = await Organization.Repository.ListAllUsersFromOrganization(organizationId);
                if (members == null)
                {
                    return null;
                }
                return members.ToList();


            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> JoinLeaveOrganization(AuthenticatedUserDTO user, long organizationId)
        {
            try
            {
                var existis = await OrganizationUser.Repository.FindExistent(user.UserId, organizationId);
                if (existis != null)
                {
                    await existis.Delete();
                }
                else
                {
                    new OrganizationUser(user, organizationId);

                }
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> DeleteOrganization(AuthenticatedUserDTO user, long organizationId)
        {
            try
            {
                var organization = await Organization.Repository.Get(organizationId);
                if (organization != null)
                {
                    await organization.Delete();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<OrganizationDTO> GetOrganization(AuthenticatedUserDTO user, long organizationId)
        {
            try
            {
                var organization = await Organization.Repository.Get(organizationId);
                if (organization != null)
                {
                    return organization.Transform<OrganizationDTO>();
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
       public async Task<List<OrganizationDTO>> ListOrganizations(AuthenticatedUserDTO user)
        {
            try
            {
                var organizations = await Organization.Repository.ListAllOrganizationOfUser(user.UserId);
                if(organizations != null)
                {
                    return organizations.ToList();
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<OrganizationDTO> EditOrganization(AuthenticatedUserDTO user, OrganizationDTO organizationDTO)
        {
            try
            {
                var organization = await Organization.Repository.Get(organizationDTO.OrganizationId);
                if (organization != null)
                {
                    await organization.Update(user,organizationDTO.Name,organizationDTO.Description,organizationDTO.Image);
                }

                return organization.Transform<OrganizationDTO>();


            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<OrganizationDTO> CreateOrganization(AuthenticatedUserDTO user, OrganizationDTO organizationDTO)
        {
            try
            {


                var organization = new Organization(user, organizationDTO.Name,organizationDTO.Description, organizationDTO.Image);



                var organiztionUser = new OrganizationUser(user, organization.OrganizationId);


                return organization.Transform<OrganizationDTO>();
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
