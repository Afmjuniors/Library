using TDCore.Core;
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Common;
using NN.Checklist.Domain.DTO.Request;
using NN.Checklist.Domain.DTO.Response;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.Services.Specifications
{
    [ObjectMap("GlobalizationService", true)]
    public interface IGlobalizationService: TDCore.Globalization.IGlobalizationService
    {

    }
}
