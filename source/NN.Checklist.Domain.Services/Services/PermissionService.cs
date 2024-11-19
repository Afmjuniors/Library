using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Entities.Parameters;
using NN.Checklist.Domain.Services.Specifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDCore.DependencyInjection;

namespace NN.Checklist.Domain.Services
{
    public class PermissionService : ObjectBase, IPermissionService
    {
        private static List<AdGroupPermissionDTO> _Permissions;


        /// <summary>
        /// Name: Initialize 
        /// Description: initialize permission system, get from database all permissions groups
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public async Task Initialize()
        {
            var par = DBParameter.Repository.Get();
            if (par != null)
            {
                var allPermissions = await AdGroupPermission.Repository.GetAllGroupPermissions();
                
                if(allPermissions != null)
                {
                    _Permissions = allPermissions.ToList();
                }
            }
        }
        /// <summary>
        /// Name: GetPermissions
        /// Description: Get permission list
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public async Task<List<AdGroupPermissionDTO>> GetPermissions()
        {
            if (_Permissions == null)
                _Permissions = new List<AdGroupPermissionDTO>();

            return _Permissions;
        }
    }
}
