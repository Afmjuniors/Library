
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System;
using System.Linq;
using iTextSharp.text;
using NN.Checklist.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;

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
            MapColumn("ItemVersionchecklistTemplateId", "item_version_checklist_template_id");
            MapColumn("Stamp", "stamp", 500);
            MapRelationshipManyToOne("Checklist", "ChecklistId", "ITEMS_CHECKLISTS", "checklist_id" );
            MapRelationshipManyToOne("CreationUser", "CreationUserId", "ITEMS_CHECKLISTS", "creation_user_id" );
            MapRelationshipManyToOne("ItemVersionchecklistTemplate", "ItemVersionchecklistTemplateId", "ITEMS_CHECKLISTS", "item_version_checklist_template_id" );

        }

        #region User Code

        public async Task<IList<ItemChecklist>> ListAllItensByChecklistIdAndIdTemplate(long checklistId, long itemTemplateId)
        {
            try
            {

            var pars = new List<SqlParameter>();
            var sql = "SELECT * from ITEMS_CHECKLISTS ic where ic.item_version_checklist_template_id = @pItemTemplateId and  ic.checklist_id = @pChecklistId Order by creation_timestamp DESC";

            SqlParameter param = new SqlParameter("pItemTemplateId", System.Data.SqlDbType.BigInt);
            param.Value = itemTemplateId;
            pars.Add(param);
            SqlParameter param2 = new SqlParameter("pChecklistId", System.Data.SqlDbType.BigInt);
            param2.Value = checklistId;
            pars.Add(param2);
            return await List<ItemChecklist>(sql,pars);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

    }
}