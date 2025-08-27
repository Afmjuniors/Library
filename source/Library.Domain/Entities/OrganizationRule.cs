using Library.Domain.Common;
using Library.Domain.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Domain;

namespace Library.Domain.Entities
{
    public class OrganizationRule : DomainBase<OrganizationRule, IOrganizationRulesRepository<OrganizationRule, System.Int64>, System.Int64>
    {

        #region Constructors
        public OrganizationRule()
        {
                
        }

        #endregion


        #region Attributes

        [AttributeDescriptor("OrganizationRuleId", true, EnumValueRanges.Positive)]
        public System.Int64 OrganizationRuleId { get; internal set; }

        [AttributeDescriptor("OrganizationId", true)]
        public System.Int64 OrganizationId { get; set; }

        [AttributeDescriptor("LoanDurationDays", true)]
        public System.Int32 LoanDurationDays { get; set; }
        [AttributeDescriptor("MeetingFrequency", true)]
        public EnumMeetingFrequency MeetingFrequency { get; set; }
        [AttributeDescriptor("MeetingDay", true)]
        public EnumMeetingDay MeetingDay { get; set; }


        [AttributeDescriptor("MeetingWeek", true)]
        public int MeetingWeek { get; set; }

        [AttributeDescriptor("MeetingTime", true)]
        public string MeetingTime { get; set; }

        [AttributeDescriptor("HasNextWeekMeeting", true)]
        public bool HasNextWeekMeeting { get; set; }
        [AttributeDescriptor("NextMeetingDate", true)]
        public DateTime NextMeetingDate { get; set; }
        [AttributeDescriptor("RenewLoanTimes", true)]
        public int RenewLoanTimes { get; set; }
        [AttributeDescriptor("CreatedAt", true)]
        public DateTime CreatedAt { get; set; }
        [AttributeDescriptor("UpdateAt", true)]
        public DateTime UpdateAt { get; set; }

        #endregion


    }
}
