
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System;
using System.Linq;
using System.Threading.Tasks;
using NN.Checklist.Domain.DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:11

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class VersionChecklistTemplateRepository: RepositoryBase<VersionChecklistTemplate, System.Int64>, IVersionChecklistTemplateRepository<VersionChecklistTemplate, System.Int64>
    {
        public VersionChecklistTemplateRepository()
        {
            MapTable("VERSIONS_CHECKLISTS_TEMPLATES");
            MapPrimaryKey("VersionChecklistTemplateId", "version_checklist_template_id",true,0);
            MapColumn("ChecklistTemplateId", "checklist_template_id");
            MapColumn("CreationUserId", "creation_user_id");
            MapColumn("TimestampCreation", "timestamp_creation");
            MapColumn("TimestampUpdate", "timestamp_update");
            MapColumn("UpdateUserId", "update_user_id");
            MapColumn("Version", "version", 10);
            MapRelationshipManyToOne("ChecklistTemplate", "ChecklistTemplateId", "VERSIONS_CHECKLISTS_TEMPLATES", "checklist_template_id" );
            MapRelationshipManyToOne("CreationUser", "CreationUserId", "VERSIONS_CHECKLISTS_TEMPLATES", "creation_user_id" );

        }

        #region User Code

        public async Task<VersionChecklistTemplateDTO> GetLatestVersionFromChecklistId( long checklistId)
        {
            try { 
            var pars = new List<SqlParameter>();
            var sql = @"SELECT TOP 1 * " +
                @" FROM VERSIONS_CHECKLISTS_TEMPLATES vct where checklist_template_id = @pChecklistId order by timestamp_creation DESC";

                SqlParameter param = new SqlParameter("pChecklistId", System.Data.SqlDbType.BigInt);
            param.Value = checklistId;
            pars.Add(param);

            return await Get<VersionChecklistTemplateDTO>(sql, pars);
            }catch (Exception ex)
            {
                throw ex;
            }

        }

        

        #endregion

    }
}