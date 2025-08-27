
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
    public class OrganizationUserRepository : RepositoryBase<OrganizationUser, System.Int64>, IOrganizationUserRepository<OrganizationUser, System.Int64>
    {
        /// <summary>
        /// Name: UserRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public OrganizationUserRepository()
        {
            MapTable("ORGANIZATIONS_USERS");
            MapPrimaryKey("OrganizationUserId", "organization_user_id", true, 0);
            MapColumn("UserId", "user_id");
            MapColumn("OrganizationId", "organization_id");



        }





        #region User Code

        public async Task<OrganizationUser> FindExistent(long userId, long organizationId)
        {
            try
            {

                var pars = new List<SqlParameter>();
                var sql = "SELECT * from ORGANIZATIONS_USERS ou where uo.user_id = @userId and organization_id = @organizationId ";

                var par = new SqlParameter("userId", System.Data.SqlDbType.BigInt);
                par.Value = userId;
                pars.Add(par);
                var par2 = new SqlParameter("organizationId", System.Data.SqlDbType.BigInt);
                par2.Value = organizationId;
                pars.Add(par2);

                var res = await Get<OrganizationUser>(sql, pars);

                return res;

            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion

    }

}
