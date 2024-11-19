using TDCore.Core;
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Common;
using NN.Checklist.Domain.DTO.Request.Parameter;
using NN.Checklist.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.Services.Specifications
{

    [ObjectMap("ParameterService", true)]
    public interface IParameterService
    {
        Task<DBParameterDTO> GetDBParameter(AuthenticatedUserDTO user);
        Task<DomainParameterDTO> GetDomainParameter(AuthenticatedUserDTO user);
        Task<PolicyParameterDTO> GetPolicyParameter(AuthenticatedUserDTO user);
        Task<MailParameterDTO> GetMailParameter(AuthenticatedUserDTO user);
        Task<MailParameterDTO> InsertMailParameter(MailParameterDTO parameters, string comments, AuthenticatedUserDTO user);
        Task<DBParameterDTO> InsertDBParameter(DBParameterDTO parameters, string comments, AuthenticatedUserDTO user);
        Task<DomainParameterDTO> InsertDomainParameter(DomainParameterDTO parameters, string comments, AuthenticatedUserDTO user);
        Task<PolicyParameterDTO> InsertPolicyParameter(PolicyParameterDTO parameters, string comments, AuthenticatedUserDTO user);
    }
}
