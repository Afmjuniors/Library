using TDCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Response
{
    public class AreaDTO
    {
        public Int64 AreaId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Int64 ProcessId { get; set; }
        public ProcessDTO Process { get; set; }
    }
}
