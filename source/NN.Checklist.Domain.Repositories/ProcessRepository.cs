
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TDCore.Data.SqlServer;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 10/2021 12:16:43

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class ProcessRepository: RepositoryBase<Process, System.Int64>, IProcessRepository<Process, System.Int64>
    {
        /// <summary>
        /// Name: ProcessRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public ProcessRepository()
        {
            MapTable("PROCESSES");
            MapPrimaryKey("ProcessId", "process_id",true,0);
            MapColumn("Acronym", "acronym", 5);
            MapColumn("Description", "description", 50);
            MapColumn("ConnectionStringAlarms", "connection_string_alarms", 1000);
            MapColumn("QueryAlarms", "query_alarms", 8000);
            MapColumn("ConnectionStringEvents", "connection_string_events", 1000);
            MapColumn("QueryEvents", "query_Events", 8000);
            MapColumn("ConnectionStringExtreme", "connection_string_extreme", 1000);
            MapColumn("QueryExtreme", "query_extreme", 8000);
            MapColumn("FirstId", "first_id");
            MapColumn("LastTimestamp", "last_timestamp");
        }

        #region User Code

        /// <summary>
        /// Name: GetByAcronym
        /// Description: Method that receives as a parameter acronym and does a search in the database and obtains by acronym.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<Process> GetByAcronym(string acronym)
        {
            try
            {
                var sql = "select * from processes with(nolock) where lower(acronym) like @pAcronym ";
                var pars = new List<SqlParameter>();

                var par = new SqlParameter("pAcronym", System.Data.SqlDbType.VarChar);
                par.Value = acronym.ToLower();
                pars.Add(par);

                return await Get<Process>(sql, pars);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}