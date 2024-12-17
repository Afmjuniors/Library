
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
    public class ChecklistTemplateRepository: RepositoryBase<ChecklistTemplate, System.Int64>, IChecklistTemplateRepository<ChecklistTemplate, System.Int64>
    {
        public ChecklistTemplateRepository()
        {
            MapTable("CHECKLISTS_TEMPLATES");
            MapPrimaryKey("ChecklistTemplateId", "checklist_template_id",true,0);
            MapColumn("Description", "description", 100);

        }

        #region User Code

        

        #endregion

    }
}