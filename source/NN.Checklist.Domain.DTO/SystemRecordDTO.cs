using TDCore.Core;
using NN.Checklist.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO
{
    public class SystemRecordDTO
    {
        public System.Int64 SystemRecordId { get; set; }

        public System.DateTime DateTime { get; set; }

        public System.String Description { get; set; }

        public System.Int64? Id { get; set; }

        public EnumSystemFunctionality SystemFunctionalityId { get; set; }

        public System.Int64? UserId { get; set; }

        public string Comments { get; set; }

        [Map("User.Initials")]
        public string UserInitials { get; set; }

        [NoMap()]
        public string FunctionalityName { 
            get
            {
                return EnumHelper.GetStringValue(SystemFunctionalityId);
            }
        }
    }
}
