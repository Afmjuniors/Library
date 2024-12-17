
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
    public class DependencyItemVersionChecklistTemplateRepository: RepositoryBase<DependencyItemVersionChecklistTemplate, System.Int64>, IDependencyItemVersionChecklistTemplateRepository<DependencyItemVersionChecklistTemplate, System.Int64>
    {
        public DependencyItemVersionChecklistTemplateRepository()
        {
            MapTable("DEPENDENCIES_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES");
            MapPrimaryKey("DependencyItemVersionChecklistTemplateId", "dependency_item_version_checklist_template_id",true,0);
            MapColumn("DependentBlockVersionChecklistTemplateId", "dependent_block_version_checklist_template_id");
            MapColumn("DependentItemVersionChecklistTemplateId", "dependent_item_version_checklist_template_id");
            MapColumn("ItemVersionChecklistTemplateId", "item_version_checklist_template_id");
            MapRelationshipManyToOne("DependentBlockVersionChecklistTemplate", "DependentBlockVersionChecklistTemplateId", "DEPENDENCIES_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES", "dependent_block_version_checklist_template_id" );
            MapRelationshipManyToOne("DependentItemVersionChecklistTemplate", "DependentItemVersionChecklistTemplateId", "DEPENDENCIES_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES", "dependent_item_version_checklist_template_id" );
            MapRelationshipManyToOne("ItemVersionChecklistTemplate", "ItemVersionChecklistTemplateId", "DEPENDENCIES_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES", "item_version_checklist_template_id" );

        }

        #region User Code

        

        #endregion

    }
}