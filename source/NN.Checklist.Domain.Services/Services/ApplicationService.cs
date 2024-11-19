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

        
        #region Process
        /// <summary>
        /// Name: "ListProcesses" 
        /// Description: method returns a list of processes.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>

        public async Task<List<ProcessDTO>> ListProcesses()
        {
            try
            {
                var list = await Process.Repository.ListAll();
                if (list != null && list.Count > 0)
                {
                    return list.TransformList<ProcessDTO>().ToList();
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

        #region Area
        /// <summary>
        /// Name: "InsertArea" 
        /// Description: method inserts the area receiving as parameter "area", "comments" and user.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<AreaDTO> InsertArea(AreaDTO area, string comments, AuthenticatedUserDTO user)
        {
            try
            {
                Area objArea = null;

                objArea = new Area(user, area.Name, area.Description, area.ProcessId, comments);

                return objArea.Transform<AreaDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Name: "UpdateArea" 
        /// Description: method updates the area by the "user", "area" and "comments" parameters.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task UpdateArea(AuthenticatedUserDTO user, AreaDTO area, string comments)
        {
            try
            {
                Area objArea = await Area.Repository.Get(area.AreaId);
                await objArea.Update(user, area.Name, area.Description, area.ProcessId, comments);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Name: "SearchAreas" 
        /// Description: method searches the areas by the "data" parameter.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<PageMessage<AreaDTO>> SearchAreas(AreaPageMessage data)
        {
            try
            {
                return await Area.Repository.Search(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Name: "ListAreas" 
        /// Description: method returns a list of "AreaDTO".
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<List<AreaDTO>> ListAreas()
        {
            try
            {
                var list = await Area.Repository.ListAll();
                if (list != null && list.Count > 0)
                {
                    return list.TransformList<AreaDTO>().ToList();
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


        /// <summary>
        /// Name: "ListAreasByProcess" 
        /// Description: method returns a list of areas per process by the "processId".
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<List<AreaDTO>> ListAreasByProcess(long processId)
        {
            try
            {
                var list = await Area.Repository.ListByProcess(processId);
                if (list != null && list.Count > 0)
                {
                    return list.TransformList<AreaDTO>().ToList();
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

        /// <summary>
        /// Name: "ListUsersByArea" 
        /// Description: method returns a list of users by area.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<List<UserDTO>> ListUsersByArea(long idArea)
        {
            try
            {
                var list = await User.Repository.ListByAreaAvailability(idArea);
                if (list != null && list.Count > 0)
                {
                    return list.TransformList<UserDTO>().ToList();
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
