using Library.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.DTO.Request
{
    public class SignUpUserDTO
    {

        public System.String Email { get; set; }
        public System.String Name { get; set; }
        public System.String Password { get; set; }
       
    }
}
