
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TDCore.Data.SqlServer;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:38

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class UserPhoneRepository: RepositoryBase<UserPhone, System.Int64>, IUserPhoneRepository<UserPhone, System.Int64>
    {
        /// <summary>
        /// Name: UserPhoneRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public UserPhoneRepository()
        {
            MapTable("USERS_PHONES");
            MapPrimaryKey("UserPhoneId", "user_phone_id",true,0);
            MapColumn("CountryId", "country_id");
            MapColumn("Number", "number", 15);
            MapColumn("UserId", "user_id");
            MapRelationshipManyToOne("Country", "CountryId", "USERS_PHONES", "country_id" );
            MapRelationshipManyToOne("User", "UserId", "USERS_PHONES", "user_id" );

        }

        #region User Code

        /// <summary>
        /// Name: ListByUser
        /// Description: Method that receives as a parameter userId, does a search in the database and lists by user.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<IList<UserPhone>> ListByUser(long userId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            var sql = @"SELECT * from USERS_PHONES AP with(nolock) where ap.user_id = @pUserId";

            var param = new SqlParameter("pUserId", System.Data.DbType.Int64);
            param.Value = userId;
            parameters.Add(param);

            return await List<UserPhone>(sql, parameters);
        }

        #endregion

    }
}