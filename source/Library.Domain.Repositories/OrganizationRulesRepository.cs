
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Domain.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Numerics;
using System.Threading.Tasks;
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:37

#endregion

namespace Library.Domain.Repositories
{
    public class OrganizationRulesRepository : RepositoryBase<OrganizationRule, System.Int64>, IOrganizationRulesRepository<OrganizationRule, System.Int64>
    {
        /// <summary>
        /// Name: UserRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public OrganizationRulesRepository()
        {
            MapTable("ORGANIZATIONS_RULES");
            MapPrimaryKey("OrganizationRuleId", "organization_rule_id", true,0);
            MapColumn("OrganizationId", "organization_id");
            MapColumn("LoanDurationDays", "loan_duration_days");
            MapColumn("MeetingFrequency", "meeting_frequency_id");
            MapColumn("MeetingDay", "meeting_day_id");
            MapColumn("MeetingWeek", "meeting_week");
            MapColumn("MeetingTime", "meeting_time");
            MapColumn("HasNextWeekMeeting", "has_next_week_meeting");
            MapColumn("NextMeetingDate", "next_meeting_date");
            MapColumn("RenewLoanTimes", "renew_loan_times");
            MapColumn("CreatedAt", "created_at");
            MapColumn("UpdateAt", "updated_at");



    }


    #region User Code




    #endregion
}
}
