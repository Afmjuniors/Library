using Library.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.DTO
{
    public class OrganizationRulesDTO
    {
        public int LoanDurationDays { get; set; }
        public EnumMeetingFrequency MeetingFrequency { get; set; }
        public EnumMeetingDay MeetingDay { get; set; }
        public int MeetingWeek {  get; set; }
        public string MeetingTime {  get; set; }
        public bool HasNextWeekMeeting {  get; set; }
        public DateTime NextMeetingDate { get; set; }
        public int? RenewLoanTimes{  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


    }
}

