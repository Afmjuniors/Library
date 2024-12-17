
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System;
using System.Linq;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:12

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class OptionFieldVersionChecklistTemplateRepository: RepositoryBase<OptionFieldVersionChecklistTemplate, System.Int64>, IOptionFieldVersionChecklistTemplateRepository<OptionFieldVersionChecklistTemplate, System.Int64>
    {
        public OptionFieldVersionChecklistTemplateRepository()
        {
            MapTable("OPTIONS_FIELDS_VERSIONS_CHECKLISTS_TEMPLATES");
            MapPrimaryKey("OptionFieldVersionChecklistTemplateId", "option_field_version_checklist_template_id",true,0);
            MapColumn("FieldVersionChecklistTemplateId", "field_version_checklist_template_id");
            MapColumn("Title", "title", 50);
            MapColumn("Value", "value");
            MapRelationshipManyToOne("FieldVersionChecklistTemplate", "FieldVersionChecklistTemplateId", "OPTIONS_FIELDS_VERSIONS_CHECKLISTS_TEMPLATES", "field_version_checklist_template_id" );

        }

        #region User Code

        

        #endregion

    }
}