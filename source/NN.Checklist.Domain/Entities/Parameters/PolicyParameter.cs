using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Repositories.Specifications.Parameters;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TDCore.DependencyInjection;
using TDCore.Domain;
using TDCore.Domain.Exceptions;

namespace NN.Checklist.Domain.Entities.Parameters
{
    [Serializable]
    public class PolicyParameter : TextFileDomainBase<PolicyParameter, IPolicyParameterRepository<PolicyParameter>>
    {
        #region Constructors

        /// <summary>
        /// Name: PolicyParameter
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public PolicyParameter()
        {

        }

        /// <summary>
        /// Name: PolicyParameter
        /// Description: Constructor method that receives as a parameter the user, inactivityTimeLimit, comments and validates the information in the validate method and inserts it into the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// Updated by: wazc CR0838177 2023-09-01
        /// Description: inclusion of notification resend time parameters, notification expiry time and maximum resend time.
        /// </summary>
        public PolicyParameter(AuthenticatedUserDTO user, long inactivityTimeLimit, string comments, long timeResendAlarmsNotification, long messageNotificationExpirationTime, long maximumNotificationResendTime)
        {
            var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
            long? userId = null;
            string language = null;

            if (user != null)
            {
                language = user.CultureInfo;
                userId = user.UserId;
            }

            var actual = Get().Result;

            InactivityTimeLimit = inactivityTimeLimit;
            TimeResendAlarmsNotification = timeResendAlarmsNotification;
            MessageNotificationExpirationTime = messageNotificationExpirationTime;
            MaximumNotificationResendTime = maximumNotificationResendTime;

            if (Validate(true).Result)
            {
                using (var tran = new TransactionScope())
                {
                    Insert().Wait();

                    tran.Complete();
                }

                var msg = globalization.GetString(globalization.DefaultLanguage, "PolicyParametersCreated", new string[] { inactivityTimeLimit.ToString(), timeResendAlarmsNotification.ToString(), messageNotificationExpirationTime.ToString(), maximumNotificationResendTime.ToString() }).Result ;

                if (actual != null)
                {
                    msg = globalization.GetString(globalization.DefaultLanguage, "PolicyParametersChanged", new string[] { actual.InactivityTimeLimit.ToString(), inactivityTimeLimit.ToString(), actual.TimeResendAlarmsNotification.ToString(), timeResendAlarmsNotification.ToString(), actual.MessageNotificationExpirationTime.ToString(), messageNotificationExpirationTime.ToString(), actual.MaximumNotificationResendTime.ToString(), maximumNotificationResendTime.ToString() }).Result;
                }

                if (user != null)
                {
                    new SystemRecord(msg, null, Common.EnumSystemFunctionality.Parameters, userId, comments);
                }
            }
        }

        #endregion

        #region Attributes
        public long InactivityTimeLimit { get; set; }
        public long TimeResendAlarmsNotification { get; set; }
        public long MessageNotificationExpirationTime { get; set; }
        public long MaximumNotificationResendTime { get; set; }

        #endregion

        #region User Code

        /// <summary>
        /// Name: Validate
        /// Description: Method that receives newRecord as a parameter, validates MessageQueuePath and checks for any errors.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// Updated by: wazc CR0838177 2023-09-01
        /// Description: validation of notification resend time parameters and notification expiry time.
        /// </summary>

        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                List<DomainError> erros = new List<DomainError>();

                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

                if (InactivityTimeLimit == 0)
                {
                    erros.Add(new DomainError("InactivityTimeLimit", await globalization.GetString("InactivityTimeInvalid")));
                }

                if(TimeResendAlarmsNotification < 1 || TimeResendAlarmsNotification > 14400)
                {
                    erros.Add(new DomainError("TimeResendAlarmsNotification", await globalization.GetString("TimeResendAlarmsNotificationInvalid")));
                }

                if(MessageNotificationExpirationTime < 0 || MessageNotificationExpirationTime > 1440)
                {
                    erros.Add(new DomainError("MessageNotificationExpirationTime", await globalization.GetString("MessageNotificationExpirationTimeInvalid")));
                }

                if (erros.Count > 0)
                {
                    throw new DomainException(await globalization.GetString("DataDomainError"), erros);
                }

                return true;
            }
            catch (DomainException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}