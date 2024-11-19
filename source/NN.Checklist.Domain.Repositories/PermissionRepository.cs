
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TDCore.Data.SqlServer;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 10/2021 12:16:45

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class PermissionRepository: RepositoryBase<Permission, System.Int64>, IPermissionRepository<Permission, System.Int64>
    {
        /// <summary>
        /// Name: PermissionRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public PermissionRepository()
        {
            MapTable("PERMISSIONS");
            MapPrimaryKey("PermissionId", "permission_id",false,0);
            MapColumn("Description", "description", 150);
            MapColumn("Name", "name", 30);
        }
        #region User Code

        /// <summary>
        /// Name: ListPermissionsByAdGroupId
        /// Description: Method that takes asGroupId as parameter and list permissions by ad group ID.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<Permission>> ListPermissionsByAdGroupId(long adGroupId)
        {
            string select = @"SELECT p.* FROM PERMISSIONS p with(nolock)
                            join AD_GROUPS_PERMISSIONS agp with(nolock) on agp.permission_id = p.permission_id
                            join AD_GROUPS ag with(nolock) on ag.ad_group_id = agp.ad_group_id
                            where ag.ad_group_id = @adGroupId";

            var pars = new List<SqlParameter>();
            var par = new SqlParameter("adGroupId", System.Data.SqlDbType.Int);
            par.Value = adGroupId;
            pars.Add(par);

            var data = await List<Permission>(select, pars);
            return data;
        }

        #endregion

    }
}