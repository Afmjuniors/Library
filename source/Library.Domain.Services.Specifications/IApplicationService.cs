using TDCore.Core;
using TDCore.Data.Paging;
using Library.Domain.DTO;
using Library.Domain.DTO.Paging;
using Library.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Services.Specifications
{
    [ObjectMap("ApplicationService", true)]
    public interface IApplicationService
    {
        #region Country
        Task<List<CountryDTO>> ListCountries();
        #endregion

        #region User
        #endregion


    }
}
