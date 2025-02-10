using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;

namespace NN.Checklist.Domain.DTO
{
    public class CommentChecklistDTO
    {
        public long CommentChecklistId {  get; set; }
        public long ChecklistId {  get; set; }
        public string Stamp { get; set; }
        public string Comments {  get; set; }
        public DateTime CreationTimestamp { get; set; }
        public long? ItemTemplateVersionId { get; set; }
        public long CreationUserId { get; set; }
        [Map("Checklist")]
        public ChecklistDTO Checklist { get; set; }
    }
}
