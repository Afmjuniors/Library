using Library.Domain.DTO;
using Library.Domain.DTO.Paging;
using Library.Domain.DTO.Response;
using Library.Domain.Entities;
using Library.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Data.Paging;
using TDCore.DependencyInjection;

namespace Library.Domain.Services
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


    
                
    }
}
