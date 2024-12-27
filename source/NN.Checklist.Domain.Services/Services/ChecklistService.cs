using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.DependencyInjection;

namespace NN.Checklist.Domain.Services
{
    public class ChecklistService : ObjectBase, IChecklistService
    {


        public async Task<object> GetCheckList(long checklistId)
        {
            var checklistTeplate = await VersionChecklistTemplate.Repository.GetLatestVersionFromChecklistId(checklistId);

            return checklistTeplate;


        }


    }
}
