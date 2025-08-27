
using Library.Domain.DTO;
using Library.Domain.Entities;
using Library.Domain.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Numerics;
using System.Threading.Tasks;
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:37

#endregion

namespace Library.Domain.Repositories
{
    public class OrganizationRepository : RepositoryBase<Organization, System.Int64>, IOrganizationRepository<Organization, System.Int64>
    {
        /// <summary>
        /// Name: UserRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public OrganizationRepository()
        {
            MapTable("ORGANIZATIONS");
            MapPrimaryKey("OrganizationId", "organization_id", true,0);
            MapColumn("Name", "name");
            MapColumn("CreatedAt", "created_at");
            MapColumn("CreatedBy", "created_by");
            MapColumn("UpdatedAt", "updated_at");
            MapColumn("UpdatedBy", "updated_by");
            MapColumn("Description", "description");
            MapColumn("Image", "image");

            MapRelationshipOneToMany("OrganizationRules", "ORGANIZATIONS_RULES", "organization_id");

        }




        #region User Code

        public async Task<IList<UserDTO>> ListAllUsersFromOrganization(long organizationId)
        {
            var pars = new List<SqlParameter>();

            var sql = "SELECT u.* from ORGANIZATIONS o " +
                "left join ORGANIZATIONS_USERS ou on ou.organization_id = o.organization_id " +
                "left join USERS u on u.user_id = ou.user_id " +
                "where o.organization_id = @organizationId " +
                "order by uo.organization_user_id desc";

            var par = new SqlParameter("organizationId", System.Data.SqlDbType.BigInt);
            par.Value = organizationId;
            pars.Add(par);

            var res =  await List<UserDTO>(sql, pars);

            return res;

        }


        public async Task<IList<OrganizationDTO>> ListAllOrganizationOfUser(long userId)
        {
            var pars = new List<SqlParameter>();

            var sql = "SELECT o.* from ORGANIZATIONS o " +
                "left join ORGANIZATIONS_USERS ou on ou.organization_id = o.organization_id " +
                "left join USERS u on u.user_id = ou.user_id " +
                "where ou.user_id = @userId " +
                "order by uo.organization_user_id desc";

            var par = new SqlParameter("userId", System.Data.SqlDbType.BigInt);
            par.Value = userId;
            pars.Add(par);

            var res = await List<OrganizationDTO>(sql, pars);

            return res;

        }


        #endregion

    }

}
