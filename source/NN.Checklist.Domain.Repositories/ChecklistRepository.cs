
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System;
using System.Linq;
using System.Threading.Tasks;
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Paging;
using System.Collections.Generic;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:12

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class ChecklistRepository : RepositoryBase<Domain.Entities.Checklist, System.Int64>, IChecklistRepository<Domain.Entities.Checklist, System.Int64>
    {
        public ChecklistRepository()
        {
            MapTable("CHECKLISTS");
            MapPrimaryKey("ChecklistId", "checklist_id", true, 0);
            MapColumn("CreationTimestamp", "creation_timestamp");
            MapColumn("CreationUserId", "creation_user_id");
            MapColumn("UpdateTimestamp", "update_timestamp");
            MapColumn("UpdateUserId", "update_user_id");
            MapColumn("VersionChecklistTemplateId", "version_checklist_template_id");
            MapRelationshipManyToOne("CreationUser", "CreationUserId", "CHECKLISTS", "creation_user_id");
            MapRelationshipManyToOne("UpdateUser", "UpdateUserId", "CHECKLISTS", "update_user_id");
            MapRelationshipManyToOne("VersionChecklistTemplate", "VersionChecklistTemplateId", "CHECKLISTS", "version_checklist_template_id");
            MapRelationshipOneToMany("Items", "ITEMS_CHECKLISTS", "checklist_id");
            MapRelationshipOneToMany("Fields", "FIELDS_CHECKLISTS", "checklist_id");

        }

        #region User Code
        /// <summary>
        /// Name: Search
        /// Description: Method that takes data as a parameter and searches the system registry for date.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<PageMessage<ChecklistDTO>> Search(ChecklistPageMessage data)
        {
            try
            {

                List<SqlParameter> parameters = new List<SqlParameter>();

                var sqlSelect = @"SELECT c.*";

                var sqlFrom = @" FROM CHECKLISTS c with (nolock)
                join VERSIONS_CHECKLISTS_TEMPLATES vct on c.version_checklist_template_id =  vct.version_checklist_template_id ";

                var sqlWhere = "";
                var and = " where ";
                if (data.StartDate.HasValue)
                {
                    sqlWhere += and + " c.creation_timestamp >= @dthStart ";
                    SqlParameter param = new SqlParameter("dthStart", System.Data.SqlDbType.DateTime);
                    param.Value = data.StartDate.Value.ToLocalTime();
                    parameters.Add(param);
                    and = " and ";
                }

                if (data.EndDate.HasValue)
                {
                    sqlWhere += and + " c.creation_timestamp < @dthEnd ";
                    SqlParameter param = new SqlParameter("dthEnd", System.Data.SqlDbType.DateTime);
                    param.Value = data.EndDate.Value.ToLocalTime();
                    parameters.Add(param);
                    and = " and ";
                }
                if (data.ChecklistTemplateId.HasValue)
                {
                    sqlWhere += and + "vct.checklist_template_id = @checklistTempateId ";
                    SqlParameter param = new SqlParameter("checklistTempateId", System.Data.SqlDbType.BigInt);
                    param.Value = data.ChecklistTemplateId.Value;
                    parameters.Add(param);
                    and = " and ";
                }
                if (data.VersionChecklistTemplateId.HasValue)
                {
                    sqlWhere += and + "c.version_checklist_template_id = @versionTempateId ";
                    SqlParameter param = new SqlParameter("versionTempateId", System.Data.SqlDbType.BigInt);
                    param.Value = data.VersionChecklistTemplateId.Value;
                    parameters.Add(param);
                    and = " and ";
                }




                var sqlOrder = " order by c.checklist_id desc ";

                return await Page<ChecklistDTO>(sqlSelect, sqlFrom, sqlWhere, sqlOrder, parameters, data, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Name: Search
        /// Description: Method that takes versionChecklistTemplateId as a parameter and list the checklists by its versions.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<Entities.Checklist>> ListChecklistByVersion(long versionChecklistTemplateId)
        {
            var pars = new List<SqlParameter>();

            var sqlSelect = @"SELECT c.*";

            var sqlFrom = @" FROM CHECKLISTS c with (nolock) " +
                "join VERSIONS_CHECKLISTS_TEMPLATES vct on c.version_checklist_template_id =  vct.version_checklist_template_id ";
            var sqlWhere = "where vct.version_checklist_template_id =  @pVersionChecklistTemplateId ";

            SqlParameter param = new SqlParameter("pVersionChecklistTemplateId", System.Data.SqlDbType.BigInt);
            param.Value = versionChecklistTemplateId;
            pars.Add(param);
            var sqlOrder = " order by c.checklist_id desc ";
            var sql = sqlSelect + sqlFrom + sqlWhere + sqlOrder;
            return await List<Entities.Checklist>(sql, pars);

        }

        #endregion

    }
}