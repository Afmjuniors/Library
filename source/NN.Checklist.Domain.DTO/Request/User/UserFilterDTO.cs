using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Request.User
{
    public class UserFilterDTO
    {
        public System.Int64 UserId { get; set; }
        public System.DateTime? DatetimeDeactivate { get; set; }
        public System.Boolean Deactivated { get; set; }
        public System.String Initials { get; set; }
        public System.Int32? LanguageId { get; set; }
    }
}
