using NN.Checklist.Domain.DTO.Request.Parameter;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Entities.Parameters;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.DependencyInjection;

namespace NN.Checklist.Domain.Services
{
    public class ParameterService : IParameterService
    {
        /// <summary>
        /// Name: "GetDBParameter" 
        /// Description: method fetches the connection parameters from the database. Getting an authenticated user as a parameter.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<DBParameterDTO> GetDBParameter(AuthenticatedUserDTO user)
        {
            var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
            try
            {
                DBParameterDTO param = DBParameter.Repository.Get().Transform<DBParameterDTO>();
                if (param == null)
                    param = new DBParameterDTO()
                    {
                        ConnectionStringSqlServer = "Server=192.168.1.12;Database=NovoScadaRemoteOficial;User Id=workteam;Password=workteam;",
                        SqlServerSchema = "NovoScadaRemoteOficial"
                    };
                return param;
            }
            catch (Exception)
            {
                throw new Exception(await globalization.GetString(user.CultureInfo, "ParameterReturnFail"));
            }
        }


        /// <summary>
        /// Name: "GetDomainParameter" 
        /// Description: method gets domain parameters, if "DomainParameter" is null. it will instantiate an object passing the admin values for the parameter.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<DomainParameterDTO> GetDomainParameter(AuthenticatedUserDTO user)
        {
            var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
            try
            {
                DomainParameterDTO param = DomainParameter.Repository.Get().Transform<DomainParameterDTO>();
                if (param == null)
                    param = new DomainParameterDTO() { 
                        AdminPassword = "Apta@2345", 
                        AdminUsername = "Admin",
                        DomainAddress = "192.188.1.11"
                    };
                return param;
            }
            catch (Exception)
            {
                throw new Exception(await globalization.GetString(user.CultureInfo, "ParameterReturnFail"));
            }
        }


        /// <summary>
        /// Name: "GetPolicyParameter" 
        /// Description: method gets the parameter policy upon receiving an authenticated user.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<PolicyParameterDTO> GetPolicyParameter(AuthenticatedUserDTO user)
        {
            var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
            try
            {
                var par = PolicyParameter.Repository.Get();
                var def = new PolicyParameterDTO() { InactivityTimeLimit = 300};
                var param = new PolicyParameterDTO();

                if (par == null)
                {
                    param = def;
                }
                else
                {
                    param.InactivityTimeLimit = par.InactivityTimeLimit;
                    param.TimeResendAlarmsNotification = par.TimeResendAlarmsNotification;
                    param.MessageNotificationExpirationTime = par.MessageNotificationExpirationTime;
                    param.MaximumNotificationResendTime = par.MaximumNotificationResendTime;
                }

                return param;
            }
            catch (Exception)
            {
                throw new Exception(await globalization.GetString(user.CultureInfo, "ParameterReturnFail"));
            }
        }


        /// <summary>
        /// Name: "GetMailParameter" 
        /// Description: method gets the email parameters.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<MailParameterDTO> GetMailParameter(AuthenticatedUserDTO user)
        {
            var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
            try
            {
                MailParameterDTO param = MailParameter.Repository.Get().Transform<MailParameterDTO>();
                return param;
            }
            catch (Exception)
            {
                throw new Exception(await globalization.GetString(user.CultureInfo, "ParameterReturnFail"));
            }
        }


        /// <summary>
        /// Name: "InsertMailParameter" 
        /// Description: method inserts the email connection parameters.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<MailParameterDTO> InsertMailParameter(MailParameterDTO parameters, string comments, AuthenticatedUserDTO user)
        {
            try
            {
                MailParameterDTO response = new MailParameter(user,
                    parameters.SMTPServer,
                    parameters.SMTPPort,
                    parameters.SMTPFromAddress, 
                    parameters.SMTPPassword, 
                    parameters.SMTPEnabledSSL, comments)
                    .Transform<MailParameterDTO>();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Name: "InsertDBParameter" 
        /// Description: method inserts the database connection parameters.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<DBParameterDTO> InsertDBParameter(DBParameterDTO parameters, string comments, AuthenticatedUserDTO user)
        {
            try
            {
                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
                DBParameterDTO response = new DBParameter(user, parameters.ConnectionStringSqlServer, parameters.SqlServerSchema, comments).Transform<DBParameterDTO>();
                
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Name: "InsertDomainParameter" 
        /// Description: method inserts the parameters domain.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<DomainParameterDTO> InsertDomainParameter(DomainParameterDTO parameters, string comments, AuthenticatedUserDTO user)
        {
            try
            {
                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

                DomainParameterDTO response = new DomainParameter(user, parameters.DomainAddress, parameters.AdminUsername, parameters.AdminPassword, comments).Transform<DomainParameterDTO>();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Name: "InsertPolicyParameter" 
        /// Description: method inserts an inactivity parameter policy.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// Updated by: wazc CR0838177 2023-09-01
        /// Description: inclusion of notification resend time parameters, notification expiry time and maximum resend time.
        /// </summary>
        public async Task<PolicyParameterDTO> InsertPolicyParameter(PolicyParameterDTO parameters, string comments, AuthenticatedUserDTO user)
        {
            try
            {
                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
                var response = new PolicyParameter(user, parameters.InactivityTimeLimit, comments, parameters.TimeResendAlarmsNotification, parameters.MessageNotificationExpirationTime, parameters.MaximumNotificationResendTime).Transform<PolicyParameterDTO>();                

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
