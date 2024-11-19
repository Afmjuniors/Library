using TDCore.Core;
using NN.Checklist.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.Services.Specifications
{

    [ObjectMap("PermissionService", true)]
    public interface IPermissionService
    {
        Task Initialize();
        Task<List<AdGroupPermissionDTO>> GetPermissions();
    }
}
