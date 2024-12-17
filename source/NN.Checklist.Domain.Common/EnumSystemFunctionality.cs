using TDCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.Common
{
    public enum EnumSystemFunctionality
    {
        Undefined = 0,
        [EnumStringValue("Systems Accesses")]
        SystemsAccesses = 1,
        [EnumStringValue("Parametes")]
        Parameters = 2,
        [EnumStringValue("Users")]
        Users = 3,
        [EnumStringValue("Permissions")]
        Permissions = 4,
        [EnumStringValue("General Registrations")]
        GeneralRegistrations = 5,
        [EnumStringValue("Checklists")]
        Checklists = 6
    }
}
