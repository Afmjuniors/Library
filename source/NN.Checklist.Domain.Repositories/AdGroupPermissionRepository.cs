
using NN.Checklist.Domain.DTO.Response;
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
    public class AdGroupPermissionRepository: RepositoryBase<AdGroupPermission, System.Int64>, IAdGroupPermissionRepository<AdGroupPermission, System.Int64>
    {        
        /// <summary>
        /// Name: AdGroupPermissionRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public AdGroupPermissionRepository()
        {
            MapTable("AD_GROUPS_PERMISSIONS");
            MapPrimaryKey("AdGroupPermissionId", "ad_group_permission_id",true,0);
            MapColumn("AdGroupId", "ad_group_id");
            MapColumn("PermissionId", "permission_id");
            MapRelationshipManyToOne("AdGroup", "AdGroupId", "AD_GROUPS_PERMISSIONS", "ad_group_id" );
            MapRelationshipManyToOne("Permission", "PermissionId", "AD_GROUPS_PERMISSIONS", "permission_id" );
        }

        #region User Code

        /// <summary>
        /// Name: GetAllGroupPermissions
        /// Description: Method that gets all group permissions.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<AdGroupPermissionDTO>> ListAllGroupPermissions()
        {
            var columns = new List<Column>();
            columns.Add(new Column() { AttributeName = "GroupName", Name = "GroupName", AttributeType = typeof(string) });
            columns.Add(new Column() { AttributeName = "GroupId", Name = "GroupId", AttributeType = typeof(long) });
            columns.Add(new Column() { AttributeName = "PermissionId", Name = "PermissionId", AttributeType = typeof(long) });

            string select = "select ad.name as GroupName, ad.ad_group_id as GroupId, adp.permission_id as PermissionId " +
                "from ad_groups_permissions adp with(nolock) inner join ad_groups ad with(nolock) on ad.ad_group_id = adp.ad_group_id";

            var data = await List<AdGroupPermissionDTO>(select, columns, null);
           
            return data;
         
        }

        /// <summary>
        /// Name: ListAdGroupPermissionsByIdAdGroup
        /// Description: Method that List ad group permissions by ad group ID
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<AdGroupPermission>> ListAdGroupPermissionsByIdAdGroup(long adGroupId)
        {
            string select = "SELECT * from AD_GROUPS_PERMISSIONS agp with(nolock) where ad_group_id = @adGroupId";

            var pars = new List<SqlParameter>();
            var par = new SqlParameter("adGroupId", System.Data.SqlDbType.Int);
            par.Value = adGroupId;
            pars.Add(par);

            var data = await List<AdGroupPermission>(select, pars);
            return data;
        }
        #endregion

    }
}