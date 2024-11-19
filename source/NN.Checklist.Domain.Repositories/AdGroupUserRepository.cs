using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TDCore.Data.SqlServer;

namespace NN.Checklist.Domain.Repositories
{
    public class AdGroupUserRepository : RepositoryBase<AdGroupUser, System.Int64>, IAdGroupUserRepository<AdGroupUser, System.Int64>
    {
        /// <summary>
        /// Name: AdGroupUserRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public AdGroupUserRepository()
        {
            MapTable("AD_GROUPS_USERS");
            MapPrimaryKey("AdGroupUserId", "ad_group_user_id", true, 0);
            MapColumn("AdGroupId", "ad_group_id");
            MapColumn("UserId", "user_id");
            MapRelationshipManyToOne("AdGroup", "AdGroupId", "AD_GROUPS_USERS", "ad_group_id");
            MapRelationshipManyToOne("User", "UserId", "AD_GROUPS_USERS", "user_id");
            MapRelationshipOneToMany("AdGroupUserAreas", "AD_GROUPS_USERS", "ad_group_user_id");
        }

        /// <summary>
        /// Name: ListAdGroupUsersByIdUser
        /// Description: Method that receives idUser as a parameter and lists the users of the abúncios group by user id.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<AdGroupUser>> ListAdGroupUsersByIdUser(long idUser)
        {
            string select = "select agu.* from AD_GROUPS_USERS agu with(nolock) where agu.user_id = @idUser";

            var pars = new List<SqlParameter>();
            var par = new SqlParameter("idUser", System.Data.SqlDbType.Int);
            par.Value = idUser;
            pars.Add(par);

            var data = await List<AdGroupUser>(select, pars);
            return data;
        }

        /// <summary>
        /// Name; ExistsByGroup
        /// Description: Method that takes adGroupId as a parameter and counts how many there are per group.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<bool> ExistsByGroup(long adGroupId)
        {
            string select = @"select count(*) from ad_groups_users with(nolock) where ad_group_id = @adGroupId";

            var pars = new List<SqlParameter>();
            var par = new SqlParameter("adGroupId", System.Data.SqlDbType.Int);
            par.Value = adGroupId;
            pars.Add(par);

            var data = await Get<long>(select, pars);
            return data > 0;
        }
    }
}