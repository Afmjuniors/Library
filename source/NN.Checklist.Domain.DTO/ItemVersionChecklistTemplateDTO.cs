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
    public class ItemVersionChecklistTemplateDTO
    {
        public long ItemVersionChecklistTemplateId { get; set; }
        public long VersionChecklistTemplateId { get; set; }
        public int Position { get; set; }
        public string Title { get; set; }
        public EnumItemType? ItemTypeId { get; set; }
        public long BlockVersionChecklistTemplateId { get; set; }
        public long? OptionFieldVersionChecklistTemplateId { get; set; }
        public string OptionsTitle { get; set; }
        public OptionFieldVersionChecklistTemplateDTO? OptionFieldVersionChecklistTemplate { get; set; }
        public List<DependencyItemVersionChecklistTemplateDTO>? DependencyItemVersionChecklistTemplate { get; set; }
        public List<OptionItemVersionChecklistTemplateDTO>? OptionItemsVersionChecklistTemplate { get; set; }

    }
}
