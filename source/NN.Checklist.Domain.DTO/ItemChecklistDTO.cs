using NN.Checklist.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Domain;

namespace NN.Checklist.Domain.DTO
{
    public class ItemChecklistDTO
    {
        public long? ItemChecklistId { get; set; }
        public long ChecklistId { get; set; }
        public long? VersionChecklistTemplateId { get; set; }
        public long ItemVersionChecklistTemplateId {  get; set; }
        [Map("ItemVersionchecklistTemplate")]
        public ItemVersionChecklistTemplateDTO? ItemVersionChecklistTemplate { get; set; }
        public string Stamp { get; set; }
        public DateTime CreationTimestamp { get; set; }
        public long CreationUserId { get; set; }
        public string? Comments { get; set; }

        public SignApprovalDTO Signature { get; set; }



    }
}
