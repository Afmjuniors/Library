using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Domain;

namespace NN.Checklist.Domain.DTO
{
    public class CancelledItemVersionChecklistTemplateDTO
    {

        public long CancelledItemVersionChecklistTemplateId { get; set; }

        public long OptionItemVersionChecklistTemplateId { get; set; }

        public long TargetItemVersionChecklistTemplateId { get; set; }

        public ItemVersionChecklistTemplateDTO TargetItemVersionChecklistTemplate { get; set; }

    }
}
