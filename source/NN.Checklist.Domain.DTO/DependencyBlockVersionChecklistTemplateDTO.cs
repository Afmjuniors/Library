using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Domain;

namespace NN.Checklist.Domain.DTO
{
    public class DependencyBlockVersionChecklistTemplateDTO
    {
        public long DependencyBlockVersionChecklistTemplateId { get; set; }
        public long BlockVersionChecklistTemplateId { get; set; }
        public long? DependentBlockVersionChecklistTemplateId { get; set; }

        public long? DependentItemVersionChecklistTemplateId { get; set; }

        [Map("BlockVersionChecklistTemplate")]
        public BlockVersionChecklistTemplateDTO BlockVersionChecklistTemplate { get; set; }
        [Map("DependentBlockVersionChecklistTemplate")]

        public BlockVersionChecklistTemplateDTO DependentBlockVersionChecklistTemplate { get; set; }
        [Map("DependentItemVersionChecklistTemplate")]

        public ItemVersionChecklistTemplateDTO DependentItemVersionChecklistTemplate { get; set; }

    }
}
