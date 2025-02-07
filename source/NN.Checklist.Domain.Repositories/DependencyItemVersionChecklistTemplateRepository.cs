
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:12

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class DependencyItemVersionChecklistTemplateRepository : RepositoryBase<DependencyItemVersionChecklistTemplate, System.Int64>, IDependencyItemVersionChecklistTemplateRepository<DependencyItemVersionChecklistTemplate, System.Int64>
    {
        public DependencyItemVersionChecklistTemplateRepository()
        {
            MapTable("DEPENDENCIES_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES");
            MapPrimaryKey("DependencyItemVersionChecklistTemplateId", "dependency_item_version_checklist_template_id", true, 0);
            MapColumn("DependentBlockVersionChecklistTemplateId", "dependent_block_version_checklist_template_id");
            MapColumn("DependentItemVersionChecklistTemplateId", "dependent_item_version_checklist_template_id");
            MapColumn("ItemVersionChecklistTemplateId", "item_version_checklist_template_id");
            MapRelationshipManyToOne("DependentBlockVersionChecklistTemplate", "DependentBlockVersionChecklistTemplateId", "DEPENDENCIES_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES", "dependent_block_version_checklist_template_id");
            MapRelationshipManyToOne("DependentItemVersionChecklistTemplate", "DependentItemVersionChecklistTemplateId", "DEPENDENCIES_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES", "dependent_item_version_checklist_template_id");

        }

        #region User Code
        public async Task<IList<DependencyItemVersionChecklistTemplate>> ListAllBLocksFromDependentBlockIdOrItemBlockId(long? blockId, long? itemId)
        {

            try
            {

                var pars = new List<SqlParameter>();
                var sql = "SELECT DISTINCT * from DEPENDENCIES_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES d ";
                var sqlWhere = "where ";
                var sqlAnd = "";


                if (itemId.HasValue && itemId > 0)
                {
                    sqlWhere += sqlAnd + " d.dependent_item_version_checklist_template_id = @pItemId ";
                    SqlParameter param = new SqlParameter("pItemId", System.Data.SqlDbType.BigInt);
                    param.Value = itemId;
                    pars.Add(param);
                    sqlAnd = "or ";

                }
                if (blockId.HasValue && blockId > 0)
                {
                    sqlWhere += sqlAnd + " d.dependent_block_version_checklist_template_id = @pBlockId ";
                    SqlParameter param2 = new SqlParameter("pBlockId", System.Data.SqlDbType.BigInt);
                    param2.Value = blockId;
                    pars.Add(param2);
                    sqlAnd = "or ";
                }
                sql += sqlWhere;

                var b = await List<DependencyItemVersionChecklistTemplate>(sql, pars);


                return await List<DependencyItemVersionChecklistTemplate>(sql, pars);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        #endregion

    }
}