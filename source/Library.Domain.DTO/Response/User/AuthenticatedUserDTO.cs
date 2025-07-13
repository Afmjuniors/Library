using Newtonsoft.Json;
using TDCore.Core;
using System.Collections.Generic;

namespace Library.Domain.DTO.Response
{
    public class AuthenticatedUserDTO
    {
        
        public long UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [NoMap]
        public string Password { get; set; }
        
        public string Token { get; set; }

        [Map("Language.Code")]
        public string CultureInfo { get; set; }


    }
}
