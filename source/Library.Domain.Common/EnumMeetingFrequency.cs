using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;

namespace Library.Domain.Common
{
    public enum EnumMeetingFrequency
    {

        [EnumStringValue("na")]
        na=0,
        [EnumStringValue("dayly")]
        dayly=1,
        [EnumStringValue("weekly")]
        weekly=2,
        [EnumStringValue("biweekly")]
        biweekly=3,
        [EnumStringValue("monthly")]
        monthly=4,
    }
}
