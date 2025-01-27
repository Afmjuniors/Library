using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO
{
    public class HistorySignatureDTO
    {
        public SignApprovalDTO Signature { get; set; }
        public List<OptionItemChecklistDTO>? OptionsSelected { get; set; }
        public List<OptionItemVersionChecklistTemplateDTO>? OptionsAvalible { get; set; }
        public bool? IsRejected { get; set; }


    }
}
