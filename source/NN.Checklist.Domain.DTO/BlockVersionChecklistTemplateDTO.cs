using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Domain;

namespace NN.Checklist.Domain.DTO
{
    public class BlockVersionChecklistTemplateDTO
    {
        public long BlockVersionChecklistTemplateId {  get; set; }
        public long? ParentBlockVersionChecklistTemplateId { get; set; }
        public int Position { get; set; }
        public string Title { get; set; }
        public long VersionChecklistTemplateId { get; set; }
        public List<ItemVersionChecklistTemplateDTO>? ItemVersionChecklistTemplate { get; set; }
        public List<DependencyItemVersionChecklistTemplateDTO>? DependenciesItemsVersionsChecklistsTemplate { get; set; }
        [Map("ParentBlockVersionChecklistTemplate")]
        public List<DependencyBlockVersionChecklistTemplateDTO>? DependenciesBlocskVersiosnChecklistsTemplateDTO { get; set; }











    }
}
