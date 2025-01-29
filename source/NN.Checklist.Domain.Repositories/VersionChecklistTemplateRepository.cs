
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
using Org.BouncyCastle.Asn1.X509;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:11

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class VersionChecklistTemplateRepository : RepositoryBase<VersionChecklistTemplate, System.Int64>, IVersionChecklistTemplateRepository<VersionChecklistTemplate, System.Int64>
    {
        public VersionChecklistTemplateRepository()
        {
            MapTable("VERSIONS_CHECKLISTS_TEMPLATES");
            MapPrimaryKey("VersionChecklistTemplateId", "version_checklist_template_id", true, 0);
            MapColumn("ChecklistTemplateId", "checklist_template_id");
            MapColumn("CreationUserId", "creation_user_id");
            MapColumn("TimestampCreation", "timestamp_creation");
            MapColumn("TimestampUpdate", "timestamp_update");
            MapColumn("UpdateUserId", "update_user_id");
            MapColumn("Version", "version", 10);
            MapRelationshipManyToOne("ChecklistTemplate", "ChecklistTemplateId", "VERSIONS_CHECKLISTS_TEMPLATES", "checklist_template_id");
            MapRelationshipManyToOne("CreationUser", "CreationUserId", "VERSIONS_CHECKLISTS_TEMPLATES", "creation_user_id");
            MapRelationshipOneToMany("BlocksChecklistTemplate", "BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES", "version_checklist_template_id");
            MapRelationshipOneToMany("FieldsVersionChecklistsTemplate", "FIELDS_VERSIONS_CHECKLISTS_TEMPLATES", "version_checklist_template_id");


        }

        #region User Code

        public async Task<VersionChecklistTemplate> GetLatestVersionFromChecklistId(long checklistId)
        {
            try
            {
                var pars = new List<SqlParameter>();
                var sql = @"SELECT TOP 1 * " +
                    @" FROM VERSIONS_CHECKLISTS_TEMPLATES vct where checklist_template_id = @pChecklistId order by timestamp_creation DESC";

                SqlParameter param = new SqlParameter("pChecklistId", System.Data.SqlDbType.BigInt);
                param.Value = checklistId;
                pars.Add(param);

                return await Get<VersionChecklistTemplate>(sql, pars);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IList<VersionChecklistTemplate>> ListVersionFromChecklistTemplateId(long checklistId)
        {
            try
            {
                var pars = new List<SqlParameter>();
                var sql = @"SELECT * " +
                    @" FROM VERSIONS_CHECKLISTS_TEMPLATES vct where checklist_template_id = @pChecklistId order by timestamp_creation DESC";

                SqlParameter param = new SqlParameter("pChecklistId", System.Data.SqlDbType.BigInt);
                param.Value = checklistId;
                pars.Add(param);

                return await List<VersionChecklistTemplate>(sql, pars);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IList<VersionChecklistTemplate>> ListVersionsByItemAndBlockId(long versionId, List<long>? blockTemplateId, List<long>? itemTemplateId)
        {
            if (blockTemplateId == null && itemTemplateId == null)
            {
                throw new Exception("NoItemAndBlockID");
            }
            var pars = new List<SqlParameter>();
            var sql = "Select DISTINCT v.* from VERSIONS_CHECKLISTS_TEMPLATES v" +
                "Join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on b.version_checklist_template_id = v.version_checklist_template_id " +
                "Join ITEMS_VERSIONS_CHECKLISTS_TEMPLATES i on i.version_checklist_template_id = v.version_checklist_template_id ";
            var where = "Where v.version_checklist_template_id <> @pVersionId ";
            var and = "and ";


            SqlParameter paramV = new SqlParameter("pVersionId", System.Data.SqlDbType.BigInt);
            paramV.Value = versionId;
            pars.Add(paramV);

            if (blockTemplateId != null && blockTemplateId.Count > 0)
            {
                var id = "";
                foreach (var blockId in blockTemplateId)
                {
                    id += blockId.ToString() + ",";
                }
                id.TrimEnd(',');

                where += and + $"b.block_version_checklist_template_id in ({id}) ";

            }
            if (itemTemplateId != null && itemTemplateId.Count > 0)
            {
                var id = "";
                foreach (var itemId in itemTemplateId)
                {
                    id += itemId.ToString() + ",";
                }
                id.TrimEnd(',');

                where += and + $"i.item_version_checklist_template_id in ({id}) ";
            }

            return await List<VersionChecklistTemplate>(sql, pars);

        }

        #endregion

    }
}