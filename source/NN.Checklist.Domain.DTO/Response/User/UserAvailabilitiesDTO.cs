using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Response.User
{
    public class UserAvailabilitiesDTO
    {
        public System.Int64 UserAvailabilityId { get; set; }
        public System.String EndTime { get; set; }
        public System.String StartTime { get; set; }
        public System.Int64 UserId { get; set; }
        public System.Int32 WeekDay { get; set; }
    }
}
