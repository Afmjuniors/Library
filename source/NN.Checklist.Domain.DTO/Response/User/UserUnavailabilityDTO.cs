using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Response.User
{
    public class UserUnavailabilityDTO
    {
        public System.Int64 UserUnavailabilityId { get; set; }
        public System.DateTime EndDate { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.Int64 UserId { get; set; }
    }
}
