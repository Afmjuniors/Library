
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 10/2021 12:16:44

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class AdGroupRepository: RepositoryBase<AdGroup, System.Int64>, IAdGroupRepository<AdGroup, System.Int64>
    {
        /// <summary>
        /// Name: AdGroupRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public AdGroupRepository()
        {
            MapTable("AD_GROUPS");
            MapPrimaryKey("AdGroupId", "ad_group_id",true,0);
            MapColumn("Name", "name", 150);
            MapColumn("Administrator", "administrator");
            MapRelationshipOneToMany("Permissions", "AD_GROUPS_PERMISSIONS", "AD_GROUP_ID");
        }

        #region User Code

        /// <summary>
        /// Name: GetByAdGroupName
        /// Description: Method that receives as a parameter, name and receives name by ad group.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<AdGroup> GetByAdGroupName(string name)
        {
            var sql = @"select * from AD_GROUPS ag with(nolock) where lower(ag.name) like @name";

            var pars = new List<SqlParameter>();
            var par = new SqlParameter("name", SqlDbType.VarChar);
            par.Value = name.ToLower();
            pars.Add(par);

            return await Get<AdGroup>(sql, pars);
        }

        /// <summary>
        /// Name: Search
        /// Description: Method that receives as a parameter, data and does a search in the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<PageMessage<AdGroupDTO>> Search(AdGroupPageMessage data)
        {
            try
            {
                var pars = new List<SqlParameter>();
                List<SqlParameter> parameters = new List<SqlParameter>();

                var sqlSelect = @"select ag.* ";

                var sqlFrom = @" from AD_GROUPS ag with(nolock) ";
                var sqlWhere = " ";

                if (data.Name != null && data.Name != "")
                {
                    sqlWhere += " WHERE ag.Name = @pName";

                    var par = new SqlParameter("pName", System.Data.SqlDbType.VarChar);
                    par.Value = data.Name;
                    pars.Add(par);
                }

                var sqlOrder = " order by ag.Name";

                var sqlCommand = sqlSelect + sqlFrom + sqlWhere + sqlOrder;

                return await Page<AdGroupDTO>(sqlSelect, sqlFrom, sqlWhere, sqlOrder, pars, data, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}