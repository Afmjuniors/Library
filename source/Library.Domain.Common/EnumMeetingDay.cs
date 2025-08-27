using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;

namespace Library.Domain.Common
{
    public enum EnumMeetingDay
    {
        
        [EnumStringValue("Sunday")]
        Sunday=1,
        [EnumStringValue("Monday")]
        Monday=2,
        [EnumStringValue("Tuesday")]
        Tuesday=3,
        [EnumStringValue("Wednesday")]
        Wednesday=4,
        [EnumStringValue("Thursday")]
        Thursday=5,
        [EnumStringValue("Friday")]
        Friday=6,
        [EnumStringValue("Saturday")]
        Saturday=7
    }
}
