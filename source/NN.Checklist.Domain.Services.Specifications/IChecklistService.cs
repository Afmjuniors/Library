using NN.Checklist.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;

namespace NN.Checklist.Domain.Services.Specifications
{
    [ObjectMap("ChecklistService", true)]
    public interface IChecklistService
    {
        Task<List<ChecklistTemplateDTO>> ListChecklist();
        Task<VersionChecklistTemplateDTO> GetLatestCheckList(long checklistId);
    }
}
