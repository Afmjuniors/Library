
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System;
using System.Linq;
using System.Threading.Tasks;
using NN.Checklist.Domain.DTO;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:12

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class ChecklistRepository: RepositoryBase<Domain.Entities.Checklist, System.Int64>, IChecklistRepository<Domain.Entities.Checklist, System.Int64>
    {
        public ChecklistRepository()
        {
            MapTable("CHECKLISTS");
            MapPrimaryKey("ChecklistId", "checklist_id",true,0);
            MapColumn("CreationTimestamp", "creation_timestamp");
            MapColumn("CreationUserId", "creation_user_id");
            MapColumn("UpdateTimestamp", "update_timestamp");
            MapColumn("UpdateUserId", "update_user_id");
            MapColumn("VersionChecklistTemplateId", "version_checklist_template_id");
            MapRelationshipManyToOne("CreationUser", "CreationUserId", "CHECKLISTS", "creation_user_id" );
            MapRelationshipManyToOne("UpdateUser", "UpdateUserId", "CHECKLISTS", "update_user_id" );
            MapRelationshipManyToOne("VersionChecklistTemplate", "VersionChecklistTemplateId", "CHECKLISTS", "version_checklist_template_id" );

        }

        #region User Code

        
        #endregion

    }
}