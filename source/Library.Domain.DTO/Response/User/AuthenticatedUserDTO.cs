using Library.Domain.Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using TDCore.Core;

namespace Library.Domain.DTO.Response
{
    public class AuthenticatedUserDTO
    {
        public System.Int64 UserId { get;  set; }
        public System.DateTime? CreatedAt { get; set; }
        public System.DateTime? BirthDay { get; set; }
        public System.String Email { get; set; }
        public System.String Name { get; set; }
        public System.String Phone { get; set; }
        public System.String Address { get; set; }
        public System.String AdditionalInfo { get; set; }
        [NoMap]
        public System.String Password { get; set; }
        public System.String Image { get; set; }
        public string Token { get; set; }

        [Map("Language.Code")]
        public string CultureInfo { get; set; }


    }
}
