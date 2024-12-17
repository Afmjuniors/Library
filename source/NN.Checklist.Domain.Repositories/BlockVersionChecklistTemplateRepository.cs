
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
    public class BlockVersionChecklistTemplateRepository: RepositoryBase<BlockVersionChecklistTemplate, System.Int64>, IBlockVersionChecklistTemplateRepository<BlockVersionChecklistTemplate, System.Int64>
    {
        public BlockVersionChecklistTemplateRepository()
        {
            MapTable("BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES");
            MapPrimaryKey("BlockVersionChecklistTemplateId", "block_version_checklist_template_id",true,0);
            MapColumn("ParentBlockVersionChecklistTemplateId", "parent_block_version_checklist_template_id");
            MapColumn("Position", "position");
            MapColumn("Title", "title", 150);
            MapColumn("VersionChecklistTemplateId", "version_checklist_template_id");
            MapRelationshipManyToOne("ParentBlockVersionChecklistTemplate", "ParentBlockVersionChecklistTemplateId", "BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES", "parent_block_version_checklist_template_id" );
            MapRelationshipManyToOne("VersionChecklistTemplate", "VersionChecklistTemplateId", "BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES", "version_checklist_template_id" );

        }

        #region User Code

        

        #endregion

    }
}