
using Library.Domain.Entities;
using Library.Domain.Repositories.Specifications;
using System.Diagnostics.Metrics;
using TDCore.Data.SqlServer;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 10/2021 12:16:47

#endregion

namespace Library.Domain.Repositories
{
    public class CountryRepository : RepositoryBase<Country, System.Int32>, ICountryRepository<Country, System.Int32>
    {
        /// <summary>
        /// Name: CountryRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public CountryRepository()
        {
            MapTable("COUNTRIES");
            MapPrimaryKey("CountryId", "country_id", true, 0);
            MapColumn("Name", "name", 50);
            MapColumn("PrefixNumber", "prefix_number");
        }

        #region User Code



        #endregion

    }
}