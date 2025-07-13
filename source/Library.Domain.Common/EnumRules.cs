using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;

namespace Library.Domain.Common
{
    public enum  EnumRules
    {
        [EnumStringValue("Repeated Metting")]
        RepeatedMeeting = 1,
        [EnumStringValue("Day of Metting")]
        DayOfMeatting = 1,
        [EnumStringValue("Days to read")]
        DaysToReady = 2
    }
}
