
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
    public class DependencyBlockVersionChecklistTemplateRepository: RepositoryBase<DependencyBlockVersionChecklistTemplate, System.Int64>, IDependencyBlockVersionChecklistTemplateRepository<DependencyBlockVersionChecklistTemplate, System.Int64>
    {
        public DependencyBlockVersionChecklistTemplateRepository()
        {
            MapTable("DEPENDENCIES_BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES");
            MapPrimaryKey("DependencyBlockVersionChecklistTemplateId", "dependency_block_version_checklist_template_id",true,0);
            MapColumn("BlockVersionChecklistTemplateId", "block_version_checklist_template_id");
            MapColumn("DependentBlockVersionChecklistTemplateId", "dependent_block_version_checklist_template_id");
            MapColumn("DependentItemVersionChecklistTemplateId", "dependent_item_version_checklist_template_id");
            MapRelationshipManyToOne("BlockVersionChecklistTemplate", "BlockVersionChecklistTemplateId", "DEPENDENCIES_BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES", "block_version_checklist_template_id" );
            MapRelationshipManyToOne("DependentBlockVersionChecklistTemplate", "DependentBlockVersionChecklistTemplateId", "DEPENDENCIES_BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES", "dependent_block_version_checklist_template_id" );
            MapRelationshipManyToOne("DependentItemVersionChecklistTemplate", "DependentItemVersionChecklistTemplateId", "DEPENDENCIES_BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES", "dependent_item_version_checklist_template_id" );

        }

        #region User Code

        

        #endregion

    }
}