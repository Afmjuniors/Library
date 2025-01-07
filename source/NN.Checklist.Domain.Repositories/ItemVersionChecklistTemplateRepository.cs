
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:11

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class ItemVersionChecklistTemplateRepository: RepositoryBase<ItemVersionChecklistTemplate, System.Int64>, IItemVersionChecklistTemplateRepository<ItemVersionChecklistTemplate, System.Int64>
    {
        public ItemVersionChecklistTemplateRepository()
        {
            MapTable("ITEMS_VERSIONS_CHECKLISTS_TEMPLATES");
            MapPrimaryKey("ItemVersionChecklistTemplateId", "item_version_checklist_template_id",true,0);
            MapColumn("BlockVersionChecklistTemplateId", "block_version_checklist_template_id");
            MapColumn("ItemTypeId", "item_type_id");
            MapColumn("OptionFieldVersionChecklistTemplateId", "option_field_version_checklist_template_id");
            MapColumn("OptionsTitle", "options_title", 500);
            MapColumn("Position", "position");
            MapColumn("Title", "title", 500);
            MapColumn("VersionChecklistTemplateId", "version_checklist_template_id");
            MapRelationshipManyToOne("BlockVersionChecklistTemplate", "BlockVersionChecklistTemplateId", "ITEMS_VERSIONS_CHECKLISTS_TEMPLATES", "block_version_checklist_template_id" );
            MapRelationshipManyToOne("ItemType", "ItemTypeId", "ITEMS_VERSIONS_CHECKLISTS_TEMPLATES", "item_type_id" );
            MapRelationshipManyToOne("OptionFieldVersionChecklistTemplate", "OptionFieldVersionChecklistTemplateId", "ITEMS_VERSIONS_CHECKLISTS_TEMPLATES", "option_field_version_checklist_template_id" );
            MapRelationshipOneToMany("DependentItemVersionChecklistTemplate", "DEPENDENCIES_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES", "item_version_checklist_template_id");
            MapRelationshipOneToMany("OptionItemsVersionChecklistTemplate", "OPTIONS_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES", "item_version_checklist_template_id");


        }

        #region User Code
        public async Task<IList<ItemVersionChecklistTemplate>> ListItemsByVersionChecklist(long versionChaklistId)
        {
            try
            {
                var pars = new List<SqlParameter>();
                var sql = @"SELECT  * " +
                    @" FROM  ITEMS_VERSIONS_CHECKLISTS_TEMPLATES ivct where version_checklist_template_id = @pVersionChecklistId";

                SqlParameter param = new SqlParameter("pVersionChecklistId", System.Data.SqlDbType.BigInt);
                param.Value = versionChaklistId;
                pars.Add(param);

                return await List<ItemVersionChecklistTemplate>(sql, pars);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        #endregion

    }
}