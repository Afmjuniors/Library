
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
    public class RegulationRepository : RepositoryBase<Regulation, System.Int64>, IRegulationRepository<Regulation, System.Int64>
    {
        /// <summary>
        /// Name: UserRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public RegulationRepository()
        {
            MapTable("REGULATIONS");
            MapPrimaryKey("RegulationId", "regulation_id", true,0);
            MapColumn("OrganizationId", "organization_id");
            MapColumn("RuleId", "rule_id");
            MapColumn("Vslue", "value");
            MapColumn("UpdatedAt", "updated_at");



        }


    #region User Code




        #endregion
    }
}
