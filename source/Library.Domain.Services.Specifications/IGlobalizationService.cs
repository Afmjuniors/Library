using TDCore.Core;
using Library.Domain.DTO;
using Library.Domain.DTO.Common;
using Library.Domain.DTO.Request;
using Library.Domain.DTO.Response;
using System.Threading.Tasks;

namespace Library.Domain.Services.Specifications
{
    [ObjectMap("GlobalizationService", true)]
    public interface IGlobalizationService: TDCore.Globalization.IGlobalizationService
    {

    }
}
