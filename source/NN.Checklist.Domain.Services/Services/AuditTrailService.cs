using NN.Checklist.Domain.Common;
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Data.Paging;

namespace NN.Checklist.Domain.Services
{
    public class AuditTrailService : IAuditTrailService
    {
        /// <summary>
        /// Name: "Search" 
        /// Description: method returns the search for "pageMessage".
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        public async Task<PageMessage<SystemRecordDTO>> Search(AuthenticatedUserDTO auth, SystemRecordPageMessage pageMessage)
        {
            
            return await SystemRecord.Repository.Search(pageMessage);
        }


        /// <summary>
        /// Name: "ListSystemFunctionalities" 
        /// Description: method returns a list of system functionalities.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<List<SystemFunctionalityDTO>> ListSystemFunctionalities()
        {
            var list = new List<SystemFunctionalityDTO>();
            foreach (var item in Enum.GetValues<EnumSystemFunctionality>())
            {
                list.Add(new SystemFunctionalityDTO() { SystemFunctionalityId = (int)item, Description = EnumHelper.GetStringValue(item) });
            }

            return list;
        }

    }
}
