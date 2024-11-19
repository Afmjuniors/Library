using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Response
{
    public class AdGroupPermissionDTO
    {
        public string GroupName { get; set; }
        public long GroupId { get; set; }
        public long PermissionId { get; set; }
    }
}
