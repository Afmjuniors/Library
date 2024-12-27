using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO
{
    public class OptionFieldVersionChecklistTemplateDTO
    {
        public long OptionFieldVersionChecklistTemplateId {  get; set; }
        public long FieldVersionChecklistTemplateId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
