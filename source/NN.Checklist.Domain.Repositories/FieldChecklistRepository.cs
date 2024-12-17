
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System;
using System.Linq;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:10

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class FieldChecklistRepository: RepositoryBase<FieldChecklist, System.Int64>, IFieldChecklistRepository<FieldChecklist, System.Int64>
    {
        public FieldChecklistRepository()
        {
            MapTable("FIELDS_CHECKLISTS");
            MapPrimaryKey("FieldChecklistId", "field_checklist_id",true,0);
            MapColumn("ChecklistId", "checklist_id");
            MapColumn("CreationTimestamp", "creation_timestamp");
            MapColumn("CreationUserId", "creation_user_id");
            MapColumn("FieldVersionChecklistTemplateId", "field_version_checklist_template_id");
            MapColumn("OptionFieldVersionChecklistTemplateId", "option_field_version_checklist_template_id");
            MapColumn("UpdateTimestamp", "update_timestamp");
            MapColumn("UpdateUserId", "update_user_id");
            MapColumn("Value", "value", 1000);
            MapRelationshipManyToOne("Checklist", "ChecklistId", "FIELDS_CHECKLISTS", "checklist_id" );
            MapRelationshipManyToOne("CreationUser", "CreationUserId", "FIELDS_CHECKLISTS", "creation_user_id" );
            MapRelationshipManyToOne("FieldVersionChecklistTemplate", "FieldVersionChecklistTemplateId", "FIELDS_CHECKLISTS", "field_version_checklist_template_id" );
            MapRelationshipManyToOne("OptionFieldVersionChecklistTemplate", "OptionFieldVersionChecklistTemplateId", "FIELDS_CHECKLISTS", "option_field_version_checklist_template_id" );
            MapRelationshipManyToOne("UpdateUser", "UpdateUserId", "FIELDS_CHECKLISTS", "update_user_id" );

        }

        #region User Code

        

        #endregion

    }
}