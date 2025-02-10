
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
    public class CommentChecklistRepository: RepositoryBase<CommentChecklist, System.Int64>, ICommentChecklistRepository<CommentChecklist, System.Int64>
    {
        public CommentChecklistRepository()
        {
            MapTable("COMMENTS_CHECKLISTS");
            MapPrimaryKey("CommentChecklistId", "comment_checklist_id",true,0);
            MapColumn("ChecklistId", "checklist_id");
            MapColumn("Comments", "comments", 5000);
            MapColumn("CreationTimestamp", "creation_timestamp");
            MapColumn("ItemTemplateVersionId", "item_template_version_id");
            MapColumn("CreationUserId", "creation_user_id");
            MapColumn("Stamp", "stamp", 500);
            MapRelationshipManyToOne("Checklist", "ChecklistId", "COMMENTS_CHECKLISTS", "checklist_id" );
            MapRelationshipManyToOne("CreationUser", "CreationUserId", "COMMENTS_CHECKLISTS", "creation_user_id" );

        }

        #region User Code

        

        #endregion

    }
}