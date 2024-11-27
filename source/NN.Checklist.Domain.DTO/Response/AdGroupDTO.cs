using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Response
{
    public class AdGroupDTO
    {
        public Int64 AdGroupId { get; set; }
        public String Name { get; set; }
        public bool Administrator { get; set; }
        public List<PermissionDTO> Permissions { get; set; }
    }
}
