using NN.Checklist.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Request.User
{
    public class UserPhoneDTO
    {
        public System.Int64 UserPhoneId { get; set; }
        public System.Int32 CountryId { get; set; }
        public System.String Number { get; set; }
        public System.Int64 UserId { get; set; }
        public CountryDTO Country { get; set; }
    }
}
