
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System;
using System.Linq;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:11

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class OptionItemChecklistRepository: RepositoryBase<OptionItemChecklist, System.Int64>, IOptionItemChecklistRepository<OptionItemChecklist, System.Int64>
    {
        public OptionItemChecklistRepository()
        {
            MapTable("OPTIONS_ITEMS_CHECKLISTS");
            MapPrimaryKey("OptionItemChecklistId", "option_item_checklist_id",true,0);
            MapColumn("CreationTimestamp", "creation_timestamp");
            MapColumn("CreationUserId", "creation_user_id");
            MapColumn("ItemChecklistId", "item_checklist_id");
            MapColumn("OptionItemVersionChecklistTemplateId", "option_item_version_checklist_template_id");
            MapRelationshipManyToOne("CreationUser", "CreationUserId", "OPTIONS_ITEMS_CHECKLISTS", "creation_user_id" );
            MapRelationshipManyToOne("ItemChecklist", "ItemChecklistId", "OPTIONS_ITEMS_CHECKLISTS", "item_checklist_id" );
            MapRelationshipManyToOne("OptionItemVersionChecklistTemplate", "OptionItemVersionChecklistTemplateId", "OPTIONS_ITEMS_CHECKLISTS", "option_item_version_checklist_template_id" );

        }

        #region User Code

        

        #endregion

    }
}