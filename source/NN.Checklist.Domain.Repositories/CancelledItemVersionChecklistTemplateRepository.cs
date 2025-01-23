
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
    public class CancelledItemVersionChecklistTemplateRepository: RepositoryBase<CancelledItemVersionChecklistTemplate, System.Int64>, ICancelledItemVersionChecklistTemplateRepository<CancelledItemVersionChecklistTemplate, System.Int64>
    {
        public CancelledItemVersionChecklistTemplateRepository()
        {
            MapTable("CANCELLED_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES");
            MapPrimaryKey("CancelledItemVersionChecklistTemplateId", "cancelled_item_version_checklist_template_id",false,0);
            MapColumn("OptionItemVersionChecklistTemplateId", "option_item_version_checklist_template_id");
            MapColumn("TargetItemVersionChecklistTemplateId", "target_item_version_checklist_template_id");
            MapRelationshipManyToOne("TargetItemVersionChecklistTemplate", "TargetItemVersionChecklistTemplateId", "CANCELLED_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES", "target_item_version_checklist_template_id" );

        }

        #region User Code

        

        #endregion

    }
}