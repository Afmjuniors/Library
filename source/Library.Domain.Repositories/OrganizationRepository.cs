
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


        }




        #region User Code


    }

    #endregion
}
