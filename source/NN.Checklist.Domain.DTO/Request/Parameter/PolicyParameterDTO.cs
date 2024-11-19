using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Request.Parameter
{
    public class PolicyParameterDTO
    {
        public long InactivityTimeLimit { get; set; }
        public long TimeResendAlarmsNotification { get; set; }
        public long MessageNotificationExpirationTime { get; set; }
        public long MaximumNotificationResendTime { get; set; }
    }
}
