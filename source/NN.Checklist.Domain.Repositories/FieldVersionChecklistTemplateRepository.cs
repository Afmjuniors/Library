
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:11

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class FieldVersionChecklistTemplateRepository: RepositoryBase<FieldVersionChecklistTemplate, System.Int64>, IFieldVersionChecklistTemplateRepository<FieldVersionChecklistTemplate, System.Int64>
    {
        public FieldVersionChecklistTemplateRepository()
        {
            MapTable("FIELDS_VERSIONS_CHECKLISTS_TEMPLATES");
            MapPrimaryKey("FieldVersionChecklistTemplateId", "field_version_checklist_template_id",true,0);
            MapColumn("FieldDataTypeId", "field_data_type_id");
            MapColumn("Format", "format", 100);
            MapColumn("IsKey", "is_key");
            MapColumn("Mandatory", "mandatory");
            MapColumn("Position", "position");
            MapColumn("RegexValidation", "regex_validation", 100);
            MapColumn("Title", "title", 100);
            MapColumn("VersionChecklistTemplateId", "version_checklist_template_id");
            MapRelationshipManyToOne("FieldDataType", "FieldDataTypeId", "FIELDS_VERSIONS_CHECKLISTS_TEMPLATES", "field_data_type_id" );
            MapRelationshipOneToMany("OptionFieldVersionChecklistTemplate", "OPTIONS_FIELDS_VERSIONS_CHECKLISTS_TEMPLATES", "field_version_checklist_template_id");

        }

        #region User Code

        public async Task<IList<FieldVersionChecklistTemplate>> ListFieldsByVersionChecklist(long versionChaklistId)
        {
            try
            {
                var pars = new List<SqlParameter>();
                var sql = @"SELECT  * " +
                    @" FROM  FIELDS_VERSIONS_CHECKLISTS_TEMPLATES ivct where version_checklist_template_id = @pVersionChecklistId";

                SqlParameter param = new SqlParameter("pVersionChecklistId", System.Data.SqlDbType.BigInt);
                param.Value = versionChaklistId;
                pars.Add(param);

                return await List<FieldVersionChecklistTemplate>(sql, pars);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}