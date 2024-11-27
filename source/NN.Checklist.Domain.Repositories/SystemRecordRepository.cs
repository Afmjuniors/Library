
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Paging;
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
//Generated: 10/2021 16:03:49

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class SystemRecordRepository: RepositoryBase<SystemRecord, System.Int64>, ISystemRecordRepository<SystemRecord, System.Int64>
    {
        /// <summary>
        /// Name: SystemRecordRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public SystemRecordRepository()
        {
            MapTable("SYSTEMS_RECORDS");
            MapPrimaryKey("SystemRecordId", "system_record_id",true,0);
            MapColumn("DateTime", "date_time");
            MapColumn("Description", "description", 8000);
            MapColumn("Id", "id");
            MapColumn("SystemFunctionalityId", "system_functionality_id");
            MapColumn("UserId", "user_id");
            MapColumn("Comments", "comments", 8000);
            MapRelationshipManyToOne("User", "UserId", "SYSTEMS_RECORDS", "user_id" );

        }

        #region User Code

        /// <summary>
        /// Name: Search
        /// Description: Method that takes data as a parameter and searches the system registry for date.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<PageMessage<SystemRecordDTO>> Search(SystemRecordPageMessage data)
        {
            try
            {
                var pars = new List<SqlParameter>();
                List<SqlParameter> parameters = new List<SqlParameter>();

                var sqlSelect = @"SELECT *";

                var sqlFrom = @" FROM systems_records s with (nolock) ";

                var sqlWhere = "";
                var and = " where ";
                if (data.StartDate.HasValue)
                {
                    sqlWhere += and + " s.DATE_TIME >= @dthStart ";
                    SqlParameter param = new SqlParameter("dthStart", System.Data.SqlDbType.DateTime);
                    param.Value = data.StartDate.Value.ToLocalTime();
                    parameters.Add(param);
                    and = " and ";
                }

                if (data.EndDate.HasValue)
                {
                    sqlWhere += and + " s.DATE_TIME < @dthEnd ";
                    SqlParameter param = new SqlParameter("dthEnd", System.Data.SqlDbType.DateTime);
                    param.Value = data.EndDate.Value.ToLocalTime();
                    parameters.Add(param);
                    and = " and ";
                }

                if (data.Functionality.HasValue)
                {
                    sqlWhere += and + " s.system_functionality_id = @systemFunctionalityId ";
                    SqlParameter param = new SqlParameter("systemFunctionalityId", System.Data.SqlDbType.Int);
                    param.Value = (int)data.Functionality.Value;
                    parameters.Add(param);
                    and = " and ";
                }

                if (data.UserId.HasValue)
                {
                    sqlWhere += and + " s.user_id = @userId ";
                    SqlParameter param = new SqlParameter("userId", System.Data.SqlDbType.BigInt);
                    param.Value = data.UserId.Value;
                    parameters.Add(param);
                    and = " and ";
                }

                if (!string.IsNullOrEmpty(data.Keyword))
                {
                    sqlWhere += and + " lower(s.description) like @keyword ";
                    SqlParameter param = new SqlParameter("keyword", System.Data.SqlDbType.VarChar);
                    param.Value = $"%{data.Keyword.ToLower()}%";
                    parameters.Add(param);
                    and = " and ";
                }

                var sqlOrder = " order by s.system_record_id desc ";

                return await Page<SystemRecordDTO>(sqlSelect, sqlFrom, sqlWhere, sqlOrder, parameters, data, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}