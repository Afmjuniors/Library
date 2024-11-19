
using TDCore.Core;
using TDCore.Data.Paging;
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Request;
using NN.Checklist.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.Services.Specifications
{

    [ObjectMap("AuditTrailService", true)]
    public interface IAuditTrailService
    {
        Task<PageMessage<SystemRecordDTO>> Search(AuthenticatedUserDTO auth, SystemRecordPageMessage pageMessage);

        Task<List<SystemFunctionalityDTO>> ListSystemFunctionalities();

    }
}
