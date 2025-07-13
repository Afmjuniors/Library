
using Library.Domain.Entities;
using Library.Domain.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TDCore.Data.SqlServer;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:37

#endregion

namespace Library.Domain.Repositories
{
    public class LanguageRepository : RepositoryBase<Language, System.Int32>, ILanguageRepository<Language, System.Int32>
    {
        /// <summary>
        /// Name: LanguageRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public LanguageRepository()
        {
            MapTable("LANGUAGES");
            MapPrimaryKey("LanguageId", "language_id", true, 0);
            MapColumn("Code", "code", 5);
            MapColumn("CountryId", "country_id");
            MapColumn("Name", "name", 100);
            MapRelationshipManyToOne("Country", "CountryId", "LANGUAGES", "country_id");

        }

        #region User Code

        /// <summary>
        /// Name: GetIdByCode
        /// Description: Method that receives lang as a parameter and does a search in the database and obtains the id by code.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<int> GetIdByCode(string lang)
        {
            try
            {
                var sqlSelect = $"select * from languages with(nolock) where UPPER(code) = @lang";
                var pars = new List<SqlParameter>();
                var par = new SqlParameter("lang", System.Data.SqlDbType.VarChar);
                par.Value = lang.ToUpper();
                pars.Add(par);
                var data = await Get<Language>(sqlSelect, pars);
                return data != null ? data.LanguageId : 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}