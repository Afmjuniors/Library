using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Domain;

namespace NN.Checklist.Domain.DTO
{
    public class DependencyItemVersionChecklistTemplateDTO
    {
        public long DependencyItemVersionChecklistTemplateId { get; set; }
        public long ItemVersionChecklistTemplateId { get; set; }
        public long? DependentBlockVersionChecklistTemplateId { get; set; }
        public long? DependentItemVersionChecklistTemplateId { get; set; }
        public long DependentVersionChecklistTemplateId { get; set; }





    }
}
