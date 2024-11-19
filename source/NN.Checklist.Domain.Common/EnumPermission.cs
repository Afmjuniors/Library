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
        ALARMS = 5,
        EVENTS = 6,
        IMPACT_ANALYSIS = 7,
        QA_Analysis = 8,
        QA_OVERVIEW = 9,
        QA_REPORT = 10,
        BATCH_REVIEW = 11,
        CONFIG_GROUPS_REGISTERS = 12,
        AUDIT_TRAIL = 13,
        MANAGE_AREAS = 14,
        CREATE_NEW_ALARM_RECORD = 15,
        CREATE_NEW_EVENT_RECORD = 16,
        SEND_APPROVED_ANALYSIS_REVIEW = 17,
        DIAGNOSTIC = 18
    }
}
