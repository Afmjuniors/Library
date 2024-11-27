using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.Common
{
    public enum EnumPermission
    {
        MANAGE_PARAMETER = 1,
        MANAGE_GROUPS = 2,
        MANAGE_LOCALIZATION = 3,
        MANAGE_USERS = 4,
        MANAGE_AREAS = 5,
        CHECKLISTS = 6,
        VALIDATIONS = 7,
        AUDIT_TRAIL = 8
    }
}
