using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using TDCore.Data.SqlServer;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 10/2021 12:16:47

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class RelationChecklistRepository : RepositoryBase<RelationChecklist, System.Int64>, IRelationChecklistRepository<RelationChecklist, System.Int64>
    {
        /// <summary>
        /// Name: CountryRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public RelationChecklistRepository()
        {
            MapTable("RELATIONS_CHECKLISTS");
            MapPrimaryKey("RelationChecklistId", "relation_checklist_id", true, 0);
            MapColumn("ChecklistId", "checklist_id");
            MapColumn("DependentChecklistId", "dependent_checklist_id");
            MapColumn("CreationTimestamp", "creation_timestamp");
            MapColumn("CreationUserId", "creation_user_id");
            MapRelationshipManyToOne("DependentChecklist", "DependentChecklistId", "CHECKLISTS", "checklist_id");
        }




        #region User Code



        #endregion

    }
}