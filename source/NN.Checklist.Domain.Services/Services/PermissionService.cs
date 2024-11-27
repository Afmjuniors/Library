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
        /// Name: GetPermissions
        /// Description: Get permission list
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public async Task<List<AdGroupPermissionDTO>> ListPermissions()
        {
            if (_Permissions == null)
            {
                _Permissions = new List<AdGroupPermissionDTO>();

                var par = DBParameter.Repository.Get();
                if (par != null)
                {
                    var allPermissions = await AdGroupPermission.Repository.ListAllGroupPermissions();

                    if (allPermissions != null)
                    {
                        _Permissions = allPermissions.ToList();
                    }
                }
            }

            return _Permissions;
        }
    }
}
