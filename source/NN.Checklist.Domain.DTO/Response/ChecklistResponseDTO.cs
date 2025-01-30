using NN.Checklist.Domain.DTO.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;

namespace NN.Checklist.Domain.DTO.Response
{
    public class ChecklistResponseDTO
    {

        public long ChecklistId { get; set; }
        public long VersionChecklistTemplateId { get; set; }
        public string ChecklistTemplateDescription { get; set; }
        public string ChecklistTemplateVersion { get; set; }        
        public DateTime CreationTimestamp { get; set; }
        public DateTime? UpdateTimestamp { get; set; }
        public UserDTO CreationUser { get; set; }
        public string Description { get; set; }
    }
}
