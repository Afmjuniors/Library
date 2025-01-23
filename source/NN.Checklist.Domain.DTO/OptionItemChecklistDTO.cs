using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Domain;

namespace NN.Checklist.Domain.DTO
{
    public class OptionItemChecklistDTO
    {
        public System.Int64 OptionItemVersionChecklistTemplateId { get; set; }
        [Map("OptionItemVersionChecklistTemplate.ItemVersionChecklistTemplateId")]
        public System.Int64 ItemVersionChecklistTemplateId { get; set; }
        [Map("OptionItemVersionChecklistTemplate.Title")]
        public System.String Title { get; set; }
        [Map("OptionItemVersionChecklistTemplate.Value")]
        public System.Int32 Value { get; set; }



    }
}
