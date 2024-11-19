using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Response
{
    public class AdGroupUserAreaDTO
    {
        public System.Int64 AdGroupUserAreaId { get; set; }
        public System.Int64 AdGroupUserId { get; set; }
        public System.Int64 AreaId { get; set; }
        public AreaDTO Area { get; set; }
    }
}
