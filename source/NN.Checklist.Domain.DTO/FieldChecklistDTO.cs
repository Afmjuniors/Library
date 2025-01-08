using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Domain;

namespace NN.Checklist.Domain.DTO
{
    public class FieldChecklistDTO
    {
        public long? FieldChecklistId { get; set; }
        public long ChecklistId { get; set; }
        public long FieldVersionChecklistTemplateId { get; set; }
        public long? OptionFieldVersionChecklistTemplateId { get; set; }
        public DateTime CreationTimestamp { get; set; }
        public long CreationUserId { get; set; }
        public DateTime? UpdateTimestamp { get; set; }
        public long? UpdateUserId { get; set; }
        public string Value { get; set; }


        public FieldVersionChecklistTemplateDTO FieldVersionChecklistTemplate { get; set; }

        public OptionFieldVersionChecklistTemplateDTO OptionFieldVersionChecklistTemplate { get; set; }

    }
}
