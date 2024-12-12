using TDCore.Core;
using TDCore.Data.Paging;
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.DTO.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.Services.Specifications
{
    [ObjectMap("ApplicationService", true)]
    public interface IApplicationService
    {
        #region Country
        Task<List<CountryDTO>> ListCountries();
        #endregion

        #region User
        #endregion

        #region AdGroups
        Task<List<AdGroupDTO>> ListAdGroups();
        #endregion

    }
}
