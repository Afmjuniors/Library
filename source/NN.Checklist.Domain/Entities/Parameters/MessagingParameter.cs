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
    public class MessagingParameter : TextFileDomainBase<MessagingParameter, IMessagingParameterRepository<MessagingParameter>>
    {

        #region Constructors

        /// <summary>
        /// Name: MessagingParameter
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public MessagingParameter()
        {

        }

        /// <summary>
        /// Name: MessagingParameter
        /// Description: Constructor method that receives as a parameter the user, messageQueuePath, comments and validates the information in the validate method and inserts it into the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        public MessagingParameter(AuthenticatedUserDTO user, string messageQueuePath, string comments)
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

            MessageQueuePath = messageQueuePath;

            if (Validate(true).Result)
            {
                using (var tran = new TransactionScope())
                {
                    Insert().Wait();

                    var msg = globalization.GetString(globalization.DefaultLanguage, "MessagingParametersCreated", new string[] { messageQueuePath }).Result;

                    if (actual != null)
                    {
                        msg = globalization.GetString(globalization.DefaultLanguage, "MessagingParametersChanged", new string[] { actual.MessageQueuePath, messageQueuePath }).Result; ;
                    }

                    if (user != null)
                    {
                        new SystemRecord(msg, null, Common.EnumSystemFunctionality.Parameters, userId, comments);
                    }

                    tran.Complete();
                }
            }
        }

        #endregion

        #region Attributes
        public string MessageQueuePath { get; set; }
        #endregion

        #region User Code

        /// <summary>
        /// Name: Validate
        /// Description: Method that receives newRecord as a parameter, validates MessageQueuePath and checks for any errors.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                List<DomainError> erros = new List<DomainError>();

                var language = ObjectFactory.GetSingleton<IGlobalizationService>();

                if (MessageQueuePath == null || MessageQueuePath.Length == 0)
                {
                }

                if (erros.Count > 0)
                {
                    throw new DomainException("Erro de consistência de dados", erros);
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
