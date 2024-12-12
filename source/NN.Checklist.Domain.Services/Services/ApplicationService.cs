using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.DTO.Response.User;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Data.Paging;
using TDCore.DependencyInjection;

namespace NN.Checklist.Domain.Services
{
    public class ApplicationService : ObjectBase, IApplicationService
    {
        #region Country
        /// <summary>
        /// Name: "ListCountries" 
        /// Description: method gets a list of countries.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<List<CountryDTO>> ListCountries()
        {
            try
            {
                var list = await Country.Repository.ListAll();
                if (list != null && list.Count > 0)
                {
                    return list.TransformList<CountryDTO>().ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
               

        #region User

        #endregion

        #region AdGroups

        /// <summary>
        /// Name: "ListAdGroups" 
        /// Description: method returns a list of the ad group.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        public async Task<List<AdGroupDTO>> ListAdGroups()
        {
            try
            {
                var list = await AdGroup.Repository.ListAll();
                if (list != null && list.Count > 0)
                {
                    return list.TransformList<AdGroupDTO>().ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
                
    }
}
