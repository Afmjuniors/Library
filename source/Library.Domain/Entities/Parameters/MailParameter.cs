using Library.Domain.DTO.Response;
using Library.Domain.Repositories.Specifications.Parameters;
using Library.Domain.Services.Specifications;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TDCore.DependencyInjection;
using TDCore.Domain;

namespace Library.Domain.Entities.Parameters
{
    [Serializable]
    public class MailParameter : TextFileDomainBase<MailParameter, IMailParameterRepository<MailParameter>>
    {
        #region Constructors
        /// <summary>
        /// Name: MailParameter
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public MailParameter()
        {

        }

        /// <summary>
        /// Name: MailParameter
        /// Description: Constructor method that receives the parameters user, smtpServer, smtpPort, smtpFromAddress, smtpPassword, smtpEnabledSSL, comments and validates the information in the validate method and inserts it into the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        public MailParameter(AuthenticatedUserDTO user, string smtpServer, int smtpPort, string smtpFromAddress, string smtpPassword, bool smtpEnabledSSL, string comments)
        {
            var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

            var actual = Get().Result;

            SMTPServer = smtpServer;
            SMTPPort = smtpPort;
            SMTPFromAddress = smtpFromAddress;
            SMTPPassword = smtpPassword;
            SMTPEnabledSSL = smtpEnabledSSL;

            if (Validate(true).Result)
            {
                using (var tran = new TransactionScope())
                {
                    Insert();

                    var msg = globalization.GetString(globalization.DefaultLanguage, "MailParametersCreated", new string[] { smtpServer, smtpPort.ToString(), smtpFromAddress, smtpPassword, SMTPEnabledSSL.ToString() }).Result;

                    if (actual != null)
                    {
                        msg = globalization.GetString(globalization.DefaultLanguage, "MailParametersChanged", new string[] { actual.SMTPServer, smtpServer, actual.SMTPPort.ToString(), smtpPort.ToString(),
                        actual.SMTPFromAddress, smtpFromAddress, smtpPassword != actual.SMTPPassword ? "changed" : "unchanged", actual.SMTPEnabledSSL.ToString(), SMTPEnabledSSL.ToString() }).Result;
                    }

       
                    tran.Complete();
                }
            }
        }

        /// <summary>
        /// Name: Validate
        /// Description: Method that takes v as a parameter and returns true.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        private async Task<bool> Validate(bool v)
        {
            return true;
        }
        #endregion

        #region Attributes
        public string SMTPServer { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPFromAddress { get; set; }
        public string SMTPPassword { get; set; }
        public bool SMTPEnabledSSL { get; set; }
        #endregion

        #region User Code
        #endregion
    }
}
