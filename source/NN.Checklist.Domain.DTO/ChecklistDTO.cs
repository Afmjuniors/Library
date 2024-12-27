using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Domain;

namespace NN.Checklist.Domain.DTO
{
    public class ChecklistDTO
    {
        public long ChecklistId { get; set; }
        public long VersionChecklistTemplateId { get; set; }
        [Map("VersionChecklistTemplate")]
        public VersionChecklistTemplateDTO? VersionChecklistTemplate { get; set; }
        public DateTime CreationTimestamp { get; set; }
        public long CreationUserId { get; set; }
        public DateTime? UpdateTimestamp { get; set; }
        public long? UpdateUserId { get; set; }
        public List<FieldChecklistDTO>? Fields { get; set; }
        public List<ItemChecklistDTO>? Items { get; set; }







    }
}
