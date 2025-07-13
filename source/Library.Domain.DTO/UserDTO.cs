using Library.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Domain;

namespace Library.Domain.DTO
{
    public class UserDTO
    {
        public System.Int64 UserId { get; internal set; }
        public System.DateTime? CreatedAt { get; set; }
        public System.DateTime? BirthDay { get; set; }
        public System.String Email { get; set; }
        public System.String Name { get; set; }
        public System.String Phone { get; set; }
        public System.String Address { get; set; }
        public System.String AdditionalInfo { get; set; }
        public System.String Password { get; set; }
        public System.String Image { get; set; }
        public EnumUserStatus UserStatusId { get; set; }
    }
}
