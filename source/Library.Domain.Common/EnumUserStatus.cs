using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;

namespace Library.Domain.Common
{
    public enum EnumUserStatus
    {

        [EnumStringValue("Active")]
        Active = 1,
        [EnumStringValue("Desactive")]
        Desactive = 2,
        [EnumStringValue("Inapt")]
        Inapt = 3
    }
}
