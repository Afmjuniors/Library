using Library.Domain.DTO.Response;
using Library.Domain.Repositories.Specifications.Parameters;
using Library.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TDCore.DependencyInjection;
using TDCore.Domain;
using TDCore.Domain.Exceptions;

namespace Library.Domain.Entities.Parameters
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
        public PolicyParameter(AuthenticatedUserDTO user, long inactivityTimeLimit, string comments)
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

            if (Validate(true).Result)
            {
                using (var tran = new TransactionScope())
                {
                    Insert().Wait();

                    tran.Complete();
                }

                var msg = globalization.GetString(globalization.DefaultLanguage, "PolicyParametersCreated", new string[] { inactivityTimeLimit.ToString()}).Result ;

                if (actual != null)
                {
                    msg = globalization.GetString(globalization.DefaultLanguage, "PolicyParametersChanged", new string[] { actual.InactivityTimeLimit.ToString(), inactivityTimeLimit.ToString() }).Result;
                }


            }
        }

        #endregion

        #region Attributes
        public long InactivityTimeLimit { get; set; }

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