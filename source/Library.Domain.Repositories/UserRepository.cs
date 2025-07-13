
using Library.Domain.Entities;
using Library.Domain.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Numerics;
using System.Threading.Tasks;
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;
using static System.Runtime.InteropServices.JavaScript.JSType;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:37

#endregion

namespace Library.Domain.Repositories
{
    public class UserRepository: RepositoryBase<User, System.Int64>, IUserRepository<User, System.Int64>
    {
        /// <summary>
        /// Name: UserRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public UserRepository()
        {
            MapTable("USERS");
            MapPrimaryKey("UserId", "user_id",true,0);
            MapColumn("Email", "email");
            MapColumn("CreatedAt", "created_at");
            MapColumn("Phone", "phone");
            MapColumn("Address", "address");
            MapColumn("AdditionalInfo", "additional_info");
            MapColumn("UserStatusId", "user_status_id");
            MapColumn("LanguageId", "language_id");
            MapColumn("Password", "password");
            MapColumn("Image", "image");
            MapColumn("Name", "name");
            MapColumn("BirthDay", "birthday");
            MapRelationshipManyToOne("Language", "LanguageId", "USERS", "language_id");



        }

        #region User Code

        public async Task<User> GetUserByEmail(string email)
        {

            List<SqlParameter> parameters = new List<SqlParameter>();
            var sql = "SELECT * from USERS  u where u.email = @email";

            SqlParameter param = new SqlParameter("email", System.Data.SqlDbType.VarChar);
            param.Value = email;
            parameters.Add(param);

            var res = await Get<User>(sql, parameters);
            return res;
        }

        #endregion
    }
}
