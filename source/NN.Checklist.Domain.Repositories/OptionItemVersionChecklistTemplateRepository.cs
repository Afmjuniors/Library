
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
    public class OptionItemVersionChecklistTemplateRepository: RepositoryBase<OptionItemVersionChecklistTemplate, System.Int64>, IOptionItemVersionChecklistTemplateRepository<OptionItemVersionChecklistTemplate, System.Int64>
    {
        public OptionItemVersionChecklistTemplateRepository()
        {
            MapTable("OPTIONS_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES");
            MapPrimaryKey("OptionItemVersionChecklistTemplateId", "option_item_version_checklist_template_id",true,0);
            MapColumn("ItemVersionChecklistTemplateId", "item_version_checklist_template_id");
            MapColumn("Title", "title", 50);
            MapColumn("Value", "value");
            MapRelationshipManyToOne("ItemVersionChecklistTemplate", "ItemVersionChecklistTemplateId", "OPTIONS_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES", "item_version_checklist_template_id" );

        }

        #region User Code

        

        #endregion

    }
}