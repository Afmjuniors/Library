using TDCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Response
{
    public class AdGroupUserDTO
    {
        [Map("AdGroup.Name")]
        public string GroupName { get; set; }
        public long AdGroupUserId { get; set; }
        public long AdGroupId { get; set; }
        public long UserId { get; set; }
        public List<AdGroupUserAreaDTO> AdGroupUserAreas { get; set; }
    }
}
