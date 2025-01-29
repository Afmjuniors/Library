using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Data.Paging;

namespace NN.Checklist.Domain.Services.Specifications
{
    [ObjectMap("ChecklistService", true)]
    public interface IChecklistService
    {
        Task<List<ChecklistTemplateDTO>> ListChecklist();
        Task<VersionChecklistTemplateDTO> GetLatestCheckList(long checklistId);
        Task<List<VersionChecklistTemplateDTO>> ListChecklistVersions(long checklistId);
        Task<ChecklistDTO> CreateUpdateChecklist(AuthenticatedUserDTO user, ChecklistDTO obj);
        Task<PageMessage<ChecklistDTO>> Search(AuthenticatedUserDTO auth, ChecklistPageMessage pageMessage);
        Task<ChecklistDTO> SignItem(AuthenticatedUserDTO auth, ItemChecklistDTO item);
        Task<List<HistorySignatureDTO>> ListAllSignuture(AuthenticatedUserDTO auth, long checklistId, long itemTemplateId);
    }
}
