using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Response
{
    public class ProcessDTO
    {
        public Int64 ProcessId { get; set; }
        public String Acronym { get; set; }
        public String Description { get; set; }
        public bool? WatchdogAlarm { get; set; }
        public bool? WatchdogEvent { get; set; }
    }
}
