using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Request
{
    public class AdGroupFilterDTO
    {
        public Int64 AdGroupId { get; set; }
        public String Name { get; set; }
        public bool Administrator { get; set; }
        public bool Maintenance { get; set; }
        public bool ImpactAnalyst { get; set; }
        public bool QAAnalyst { get; set; }
    }
}
