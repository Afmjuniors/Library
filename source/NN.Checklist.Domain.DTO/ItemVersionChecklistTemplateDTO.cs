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
        public int position { get; set; }
        public string Title { get; set; }
        public EnumItemType? ItemTypeId { get; set; }
        public long BlockVersionChecklistTemplateId { get; set; }
        public long? OptionFieldVersionChecklistTemplateId { get; set; }
        public string OptionsTitle { get; set; }
        [Map("BlockVersionChecklistTemplate")]
        public BlockVersionChecklistTemplateDTO? BlockVersionChecklistTemplate { get; set; }
        [Map("OptionFieldVersionChecklistTemplate")]
        public OptionFieldVersionChecklistTemplateDTO? OptionFieldVersionChecklistTemplate { get; set; }
        [Map("VersionChecklistTemplate")]
        public VersionChecklistTemplateDTO? VersionChecklistTemplate { get; set; }
        public ItemVersionChecklistTemplateDTO? DependencyItemVersionChecklistTemplate { get; set; }

    }
}
