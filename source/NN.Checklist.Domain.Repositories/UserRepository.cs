
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Response.User;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:37

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class UserRepository: RepositoryBase<User, System.Int64>, IUserRepository<User, System.Int64>
    {
        /// <summary>
        /// Name: UserRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public UserRepository()
        {
            MapTable("USERS");
            MapPrimaryKey("UserId", "user_id",true,0);
            MapColumn("DatetimeDeactivate", "datetime_deactivate");
            MapColumn("Deactivated", "deactivated");
            MapColumn("Initials", "initials", 30);
            MapColumn("LanguageId", "language_id");
            MapRelationshipManyToOne("Language", "LanguageId", "USERS", "language_id");
        }

        #region User Code
        /// <summary>
        /// Name: "GetByInitials" 
        /// Description: method returns the user's initials.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<User> GetByInitials(string initials)
        {
            try
            {
                var sqlSelect = $"select * from users u with(nolock) where initials = @pInitials";

                var pars = new List<SqlParameter>();
                var par = new SqlParameter("pInitials", System.Data.SqlDbType.VarChar);
                par.Value = initials;
                pars.Add(par);
                var data = await Get<User>(sqlSelect, pars);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Name: "ListByAreaAvailability" 
        /// Description: method returns a list per available area.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<User>> ListByAreaAvailability(long idArea)
        {
            string select = "select * from USERS u with(nolock) " +
                    "join AD_GROUPS_USERS agu with(nolock) on u.user_id = agu.user_id " +
                    "join AD_GROUPS_USERS_AREAS agua with(nolock) on agu.ad_group_user_id = agua.ad_group_user_id " +
                    "join AD_GROUPS ag with(nolock) on agu.ad_group_id = ag.ad_group_id " +
                    "join AREAS a with(nolock) on a.area_id = agua.area_id " +
                    "join USERS_AVAILABILITIES uav with(nolock) on u.user_id = uav.user_id " +
                    "left join USERS_UNAVAILABILITIES uua with(nolock) on u.user_id = uua.user_id and (uua.start_date < GETDATE() and uua.end_date > GETDATE())" +
                    "where a.area_id = @idArea " +
                    "and u.deactivated = 0" +
                    "and (uav.week_day = DATEPART(WEEKDAY, GETDATE()) and " +
                    "cast(datepart(hour, CONVERT(TIME, uav.start_time)) * 60 + datepart(minute, CONVERT(TIME, uav.start_time)) as float) <= " +
                    "cast(datepart(hour, GETDATE()) * 60 + datepart(minute, GETDATE()) as float) and " +
                    "cast(datepart(hour, CONVERT(TIME, uav.end_time)) * 60 + datepart(minute, CONVERT(TIME, uav.end_time)) as float) >= " +
                    "cast(datepart(hour, GETDATE()) * 60 + datepart(minute, GETDATE()) as float)) " +
                    " and ag.maintenance = 1 and uua.user_unavailability_id is null";

            var pars = new List<SqlParameter>();
            var par = new SqlParameter("idArea", System.Data.SqlDbType.Int);
            par.Value = idArea;
            pars.Add(par);

            var data = await List<User>(select, pars);
            return data;
        }


        /// <summary>
        /// Name: "ListMains" 
        /// Description: method returns a list of networks by the select made.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<User>> ListMains(long idArea)
        {
            string select = "select * from USERS u with(nolock) " +
                    "join AD_GROUPS_USERS agu with(nolock) on u.user_id = agu.user_id " +
                    "join AD_GROUPS_USERS_AREAS agua with(nolock) on agu.ad_group_user_id = agua.ad_group_user_id " +
                    "join AD_GROUPS ag with(nolock) on agu.ad_group_id = ag.ad_group_id " +
                    "join AREAS a with(nolock) on a.area_id = agua.area_id " +
                    "where a.area_id = @idArea " +
                    " and u.deactivated = 0 " +
                    " and ag.maintenance = 1 ";

            var pars = new List<SqlParameter>();
            var par = new SqlParameter("idArea", System.Data.SqlDbType.Int);
            par.Value = idArea;
            pars.Add(par);

            var data = await List<User>(select, pars);
            return data;
        }


        /// <summary>
        /// Name: "ListQAByAreaAvailability" 
        /// Description: method returns a list of availability per area by the select made.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<User>> ListQAByAreaAvailability(long idArea)
        {
            string select = "select * from USERS u with(nolock) " +
                    "join AD_GROUPS_USERS agu with(nolock) on u.user_id = agu.user_id " +
                    "join AD_GROUPS_USERS_AREAS agua with(nolock) on agu.ad_group_user_id = agua.ad_group_user_id " +
                    "join AD_GROUPS ag with(nolock) on agu.ad_group_id = ag.ad_group_id " +
                    "join AREAS a with(nolock) on a.area_id = agua.area_id " +
                    "join USERS_AVAILABILITIES uav with(nolock) on u.user_id = uav.user_id " +
                    "left join USERS_UNAVAILABILITIES uua with(nolock) on u.user_id = uua.user_id and (uua.start_date < GETDATE() and uua.end_date > GETDATE())" +
                    "where a.area_id = @idArea " +
                    "and u.deactivated = 0 " +
                    "and (uav.week_day = DATEPART(WEEKDAY, GETDATE()) and " +
                    "cast(datepart(hour, CONVERT(TIME, uav.start_time)) * 60 + datepart(minute, CONVERT(TIME, uav.start_time)) as float) <= " +
                    "cast(datepart(hour, GETDATE()) * 60 + datepart(minute, GETDATE()) as float) and " +
                    "cast(datepart(hour, CONVERT(TIME, uav.end_time)) * 60 + datepart(minute, CONVERT(TIME, uav.end_time)) as float) >= " +
                    "cast(datepart(hour, GETDATE()) * 60 + datepart(minute, GETDATE()) as float)) " +
                    " and ag.qa_analyst = 1 and uua.user_unavailability_id is null";

            var pars = new List<SqlParameter>();
            var par = new SqlParameter("idArea", System.Data.SqlDbType.Int);
            par.Value = idArea;
            pars.Add(par);

            var data =  await List<User>(select, pars);
            return data;
        }


        /// <summary>
        /// Name: "ListQAs" 
        /// Description: returns a list of QAs methods by select.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<User>> ListQAs(long? idArea)
        {
            string select = "select distinct u.* from USERS u with(nolock) " +
                    "join AD_GROUPS_USERS agu with(nolock) on u.user_id = agu.user_id " +
                    "join AD_GROUPS_USERS_AREAS agua with(nolock) on agu.ad_group_user_id = agua.ad_group_user_id " +
                    "join AD_GROUPS ag with(nolock) on agu.ad_group_id = ag.ad_group_id " +
                    "join AREAS a with(nolock) on a.area_id = agua.area_id " +
                    "where ag.qa_analyst = 1 " +
                    "and u.deactivated  = 0";

            var pars = new List<SqlParameter>();

            if (idArea.HasValue)
            {
                select += " and a.area_id = @idArea ";
                var par = new SqlParameter("idArea", System.Data.SqlDbType.Int);
                par.Value = idArea;
                pars.Add(par);
            }

            var data = await List<User>(select, pars);
            return data;
        }


        /// <summary>
        /// Name: "ListImpactAnalystByAreaAvailability" 
        /// Description: method returns a list of impacts per area with availability by select.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<User>> ListImpactAnalystByAreaAvailability(long idArea)
        {
            string select = "select * from USERS u with(nolock) " +
                    "join AD_GROUPS_USERS agu with(nolock) on u.user_id = agu.user_id " +
                    "join AD_GROUPS_USERS_AREAS agua with(nolock) on agu.ad_group_user_id = agua.ad_group_user_id " +
                    "join AD_GROUPS ag with(nolock) on agu.ad_group_id = ag.ad_group_id " +
                    "join AREAS a with(nolock) on a.area_id = agua.area_id " +
                    "join USERS_AVAILABILITIES uav with(nolock) on u.user_id = uav.user_id " +
                    "left join USERS_UNAVAILABILITIES uua with(nolock) on u.user_id = uua.user_id  and (uua.start_date < GETDATE() and uua.end_date > GETDATE())" +
                    "where a.area_id = @idArea " +
                    "and (uav.week_day = DATEPART(WEEKDAY, GETDATE()) and " +
                    "cast(datepart(hour, CONVERT(TIME, uav.start_time)) * 60 + datepart(minute, CONVERT(TIME, uav.start_time)) as float) <= " +
                    "cast(datepart(hour, GETDATE()) * 60 + datepart(minute, GETDATE()) as float) and " +
                    "cast(datepart(hour, CONVERT(TIME, uav.end_time)) * 60 + datepart(minute, CONVERT(TIME, uav.end_time)) as float) >= " +
                    "cast(datepart(hour, GETDATE()) * 60 + datepart(minute, GETDATE()) as float)) " +
                    " and ag.impact_analyst = 1 and uua.user_unavailability_id is null";

            var pars = new List<SqlParameter>();
            var par = new SqlParameter("idArea", System.Data.SqlDbType.Int);
            par.Value = idArea;
            pars.Add(par);

            var data = await List<User>(select, pars);
            return data;
        }


        /// <summary>
        /// Name: "ListImpactsAnalyst" 
        /// Description: method returns a list of analyzed impacts.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<User>> ListImpactsAnalysts(long idArea)
        {
            string select = "select * from USERS u with(nolock) " +
                    "join AD_GROUPS_USERS agu with(nolock) on u.user_id = agu.user_id " +
                    "join AD_GROUPS_USERS_AREAS agua with(nolock) on agu.ad_group_user_id = agua.ad_group_user_id " +
                    "join AD_GROUPS ag with(nolock) on agu.ad_group_id = ag.ad_group_id " +
                    "join AREAS a with(nolock) on a.area_id = agua.area_id " +
                    "where a.area_id = @idArea " +
                    " and ag.impact_analyst = 1 " +
                    "and u.deactivated = 0 ";

            var pars = new List<SqlParameter>();
            var par = new SqlParameter("idArea", System.Data.SqlDbType.Int);
            par.Value = idArea;
            pars.Add(par);

            var data = await List<User>(select, pars);
            return data;
        }

        /// <summary>
        /// Name: "ListByAdministratorGroup" 
        /// Description: method returns a list of administrator groups.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<User>> ListByAdministratorGroup()
        {
            string select = "select * from USERS u  " +
                    "join AD_GROUPS_USERS agu with(nolock) on u.user_id = agu.user_id " +
                    "join AD_GROUPS ag with(nolock) on agu.ad_group_id = ag.ad_group_id and ag.administrator = 1 " +
                    "join USERS_AVAILABILITIES uav with(nolock) on u.user_id = uav.user_id " +
                    "left join USERS_UNAVAILABILITIES uua with(nolock) on u.user_id = uua.user_id and (uua.start_date < GETDATE() and uua.end_date > GETDATE())" +
                    "where " +
                    "(uav.week_day = DATEPART(WEEKDAY, GETDATE()) and " +
                    "cast(datepart(hour, CONVERT(TIME, uav.start_time)) * 60 + datepart(minute, CONVERT(TIME, uav.start_time)) as float) <= " +
                    "cast(datepart(hour, GETDATE()) * 60 + datepart(minute, GETDATE()) as float) and " +
                    "cast(datepart(hour, CONVERT(TIME, uav.end_time)) * 60 + datepart(minute, CONVERT(TIME, uav.end_time)) as float) >= " +
                    "cast(datepart(hour, GETDATE()) * 60 + datepart(minute, GETDATE()) as float)) " +
                    "and uua.user_unavailability_id is null " +
                    "and u.deactivated = 0";

            var data = await List<User>(select, null);
            return data;
        }

        /// <summary>
        /// Name: "ListAdministrators" 
        /// Description: method returns a list of administrators per sql.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<User>> ListAdministrators()
        {
            string select = "select * from USERS u with(nolock) " +
                    "join AD_GROUPS_USERS agu with(nolock) on u.user_id = agu.user_id " +
                    "join AD_GROUPS ag  with(nolock) on agu.ad_group_id = ag.ad_group_id and ag.administrator = 1 " +
                    "and u.deactivated = 0";

            var data = await List<User>(select, null);
            return data;
        }


        /// <summary>
        /// Name: "ListByAreaOccurrence" 
        /// Description: method returns a list of occurrences by area by sql.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<User>> ListByAreaOccurrence(long idArea, long idOccurrence, bool? includeQa)
        {
            string select = @"select DISTINCT u.* from AREAS a with(nolock) 
                            join IMPACTED_AREAS ia with(nolock) on ia.area_id = a.area_id
                            join TYPES_OCCURRENCES_RECORDS tor with(nolock) on tor.type_occurrence_record_id = ia.type_occurrence_record_id
                            JOIN OCCURRENCES_RECORDS or2 with(nolock) on or2.type_occurrence_record_id = tor.type_occurrence_record_id
                            join AD_GROUPS_USERS_AREAS ada with(nolock) on ada.area_id = a.area_id
                            join AD_GROUPS_USERS agu with(nolock) on agu.ad_group_user_id = ada.ad_group_user_id
                            join USERS u with(nolock) on u.user_id = agu.user_id
                            join AD_GROUPS ag on agu.ad_group_id  = ag.ad_group_id 
                            where or2.occurrence_record_id = @idOccurrence 
                            and ia.area_id = @idArea
                            ";

            if(includeQa == true)
            {
                select += " and ag.qa_analyst = 1";
            }
            else
            {
                select += "and ag.impact_analyst = 1";

            }

            var pars = new List<SqlParameter>();
            var par = new SqlParameter("idArea", System.Data.SqlDbType.Int);
            par.Value = idArea;
            pars.Add(par);


            par = new SqlParameter("idOccurrence", System.Data.SqlDbType.Int);
            par.Value = idOccurrence;
            pars.Add(par);

            var data = await List<User>(select, pars);
            return data;
        }


        /// <summary>
        /// Name: "Search" 
        /// Description: method does a search by select sorting by initials.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public async Task<PageMessage<UserDTO>> Search(UsersPageMessage data)
        {
            try
            {
                var pars = new List<SqlParameter>();
                List<SqlParameter> parameters = new List<SqlParameter>();

                var sqlSelect = @"SELECT U.*";

                var sqlFrom = @" FROM USERS U with (nolock)";
                var sqlWhere = " where initials <> '_adm' ";

                if (data.Initials != null && data.Initials != "")
                {
                    sqlWhere += " and U.initials = @pInitials";

                    var par = new SqlParameter("pInitials", System.Data.SqlDbType.VarChar);
                    par.Value = data.Initials;
                    pars.Add(par);
                }

                
                
                var sqlOrder = " order by U.initials";

                var sqlCommand = sqlSelect + sqlFrom + sqlWhere + sqlOrder;

                return await Page<UserDTO>(sqlSelect, sqlFrom, sqlWhere, sqlOrder, pars, data, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}