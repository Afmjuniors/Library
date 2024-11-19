using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Response
{
    public class ItemMenuDTO
    {
        public string? Title { get; set; }
        public bool? Root { get; set; }
        public string? Alignment { get; set; }
        public string? Toggle { get; set; }
        public string? Bullet { get; set; }
        public string? Icon { get; set; }
        public string? Page { get; set; }
        public string? Translate { get; set; }
        public bool? Highlight { get; set; }
        public List<ItemMenuDTO> Submenu { get; set; }
        public long? AdGroupId { get; set; }
        public long? PermissionId { get; set; }
    }
}
