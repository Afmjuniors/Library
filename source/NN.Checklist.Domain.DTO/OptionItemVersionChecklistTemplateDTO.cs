using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Domain;

namespace NN.Checklist.Domain.DTO
{
    public class OptionItemVersionChecklistTemplateDTO
    {
        public System.Int64 OptionItemVersionChecklistTemplateId { get;  set; }

        public System.Int64 ItemVersionChecklistTemplateId { get; set; }

        public System.String Title { get; set; }

        public System.Int32 Value { get; set; }

    }
}
