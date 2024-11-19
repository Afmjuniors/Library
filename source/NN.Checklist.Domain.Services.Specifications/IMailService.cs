using TDCore.Core;
using NN.Checklist.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.Services.Specifications
{
    [ObjectMap("MailService", true)]

    public interface IMailService
    {
        Task SendExportedAlertFromMail(AuthenticatedUserDTO user, string filePath, string fileName);
        Task SendExportedAlertFromMail(AuthenticatedUserDTO user, string email, string filePath, string fileName, string searchingParameters);
    }
}
