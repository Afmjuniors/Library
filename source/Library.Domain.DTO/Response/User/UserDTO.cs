using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.DTO.Response.User
{
    public class UserDTO
    {
        public System.Int64 UserId { get; set; }
        public System.DateTime? DatetimeDeactivate { get; set; }
        public System.Boolean Deactivated { get; set; }
        public System.String Initials { get; set; }
        public System.Int32? LanguageId { get; set; }
        //public LanguageDTO Language { get; set; }
        public bool Active { get; set; }
    }
}
