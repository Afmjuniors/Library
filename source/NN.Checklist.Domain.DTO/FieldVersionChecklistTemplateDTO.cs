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
    public class FieldVersionChecklistTemplateDTO
    {
        public long FieldVersionChecklistTemplateId {  get; set; }
        public long VersionChecklistTemplateId { get; set; }
        public int Position { get; set; }
        public string Title { get; set; }
        public EnumFieldDataType FieldDataTypeId { get; set; }
        public string RegexValidation {  get; set; }
        public string Format { get; set; }
        public bool Mandatory { get; set; }
        public bool IsKey { get; set; }

        public List<OptionFieldVersionChecklistTemplateDTO>? OptionFieldVersionChecklistTemplate { get; set; }










    }
}
