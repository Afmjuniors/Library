
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
    public class ItemChecklistRepository: RepositoryBase<ItemChecklist, System.Int64>, IItemChecklistRepository<ItemChecklist, System.Int64>
    {
        public ItemChecklistRepository()
        {
            MapTable("ITEMS_CHECKLISTS");
            MapPrimaryKey("ItemChecklistId", "item_checklist_id",true,0);
            MapColumn("ChecklistId", "checklist_id");
            MapColumn("Comments", "comments", 5000);
            MapColumn("CreationTimestamp", "creation_timestamp");
            MapColumn("CreationUserId", "creation_user_id");
            MapColumn("ItemVersionchecklistTemplateId", "item_versionchecklist_template_id");
            MapColumn("Stamp", "stamp", 500);
            MapRelationshipManyToOne("Checklist", "ChecklistId", "ITEMS_CHECKLISTS", "checklist_id" );
            MapRelationshipManyToOne("CreationUser", "CreationUserId", "ITEMS_CHECKLISTS", "creation_user_id" );
            MapRelationshipManyToOne("ItemVersionchecklistTemplate", "ItemVersionchecklistTemplateId", "ITEMS_CHECKLISTS", "item_versionchecklist_template_id" );

        }

        #region User Code

        

        #endregion

    }
}