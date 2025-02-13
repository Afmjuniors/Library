using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;

namespace NN.Checklist.Domain.DTO
{
    public class VersionChecklistTemplateDTO
    {
        public long VersionChecklistTemplateId { get; set; }
        public long ChecklistTemplateId { get; set; }

        [Map("ChecklistTemplate.Description")]
        public string ChecklistTemplateDescription { get; set; }

        public string Version { get; set; }
        public DateTime TimestampCreation { get; set; }
        public long CreationUserId { get; set; }
        public DateTime? TimestampUpdate { get; set; }
        public long? UpdateUserId { get; set; }

        public List<FieldVersionChecklistTemplateDTO>? FieldsVersionChecklistsTemplate { get; set; }
        public VersionChecklistTemplateDTO? DependentVersionChecklistTemplate { get; set; }
        public List<BlockVersionChecklistTemplateDTO> BlocksTree {  get; set; }
    }
}
