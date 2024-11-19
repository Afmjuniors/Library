
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Response;
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
//Generated: 10/2021 12:16:42

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class AreaRepository: RepositoryBase<Area, System.Int64>, IAreaRepository<Area, System.Int64>
    {
        /// <summary>
        /// Name: AreaRepository
        /// Description:  class constructor
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public AreaRepository()
        {
            MapTable("AREAS");
            MapPrimaryKey("AreaId", "area_id",true,0);
            MapColumn("Name", "name", 50);
            MapColumn("Description", "description", 150);
            MapColumn("ProcessId", "process_id");
            MapRelationshipManyToOne("Process", "ProcessId", "AREAS", "process_id" );
            MapRelationshipOneToMany("Phones", "AREAS", "area_id");
        }

        #region User Code

        /// <summary>
        /// Name: Search
        /// Description: Method that receives data as a parameter and searches the database and returns the value.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<PageMessage<AreaDTO>> Search(AreaPageMessage data)
        {
            try
            {
                var pars = new List<SqlParameter>();
                List<SqlParameter> parameters = new List<SqlParameter>();

                var sqlSelect = @"SELECT A.*";

                var sqlFrom = @" FROM AREAS A with(nolock) ";
                var sqlWhere = "";

                if (data.Description != null || data.Name != null || data.ProcessId > 0)
                {
                    sqlWhere = " WHERE ";
                    bool addAnd = false;
                    if (data.Description != null && data.Description != "")
                    {
                        sqlWhere += " A.description = @pDescription";

                        var par = new SqlParameter("pDescription", System.Data.SqlDbType.VarChar);
                        par.Value = data.Description;
                        parameters.Add(par);
                        addAnd = true;
                    }

                    if (data.Name != null)
                    {
                        if (addAnd)
                        {
                            sqlWhere += " AND ";
                        }
                        sqlWhere += " A.NAME = @pName ";
                        var par = new SqlParameter("pName", System.Data.SqlDbType.VarChar);
                        par.Value = data.Name;
                        parameters.Add(par);
                        addAnd = true;
                    }

                    if (data.ProcessId > 0)
                    {
                        if (addAnd)
                        {
                            sqlWhere += " AND ";
                        }
                        sqlWhere += " A.process_id = @processId ";
                        SqlParameter param = new SqlParameter("processId", System.Data.SqlDbType.BigInt);
                        param.Value = data.ProcessId;
                        parameters.Add(param);
                        addAnd = true;
                    }
                }

                var sqlOrder = " order by A.NAME";

                var sqlCommand = sqlSelect + sqlFrom + sqlWhere + sqlOrder;

                return await Page<AreaDTO>(sqlSelect, sqlFrom, sqlWhere, sqlOrder, parameters, data, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Name: GetByName
        /// Description: Method that receives processId, name as a parameter and searches the database for the area by name.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<Area> GetByName(long processId, string name)
        {
            try
            {
                var sql = "select * from areas with(nolock) where lower(name) = @pName and process_id = @pProcessId ";
                var pars = new List<SqlParameter>();

                var par = new SqlParameter("pName", System.Data.SqlDbType.VarChar);
                par.Value = name.ToLower();
                pars.Add(par);

                var par2 = new SqlParameter("pProcessId", System.Data.SqlDbType.BigInt);
                par2.Value = processId;
                pars.Add(par2);

                return await Get<Area>(sql, pars);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Name: ListByProcess
        /// Description: Method that receives processId as a parameter and searches the database and lists it by process.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<Area>> ListByProcess(long processId)
        {
            try
            {
                var sql = "select * from areas with(nolock) where process_id = @pProcessId order by name, description";
                var pars = new List<SqlParameter>();

                var par2 = new SqlParameter("pProcessId", System.Data.SqlDbType.BigInt);
                par2.Value = processId;
                pars.Add(par2);

                return await List<Area>(sql, pars);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

    }
}