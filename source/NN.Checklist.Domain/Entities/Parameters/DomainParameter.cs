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
    public class DomainParameter : TextFileDomainBase<DomainParameter, IDomainParameterRepository<DomainParameter>>
    {

        #region Constructors

        /// <summary>
        /// Name: DomainParameter
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public DomainParameter()
        {

        }

        /// <summary>
        /// Name: DomainParameter
        /// Description: Constructor method that receives the parameters user, address, username, pass, comments and validates the information in the validate method and inserts it into the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        public DomainParameter(AuthenticatedUserDTO user, string address, string username, string pass, string comments)
        {
            var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

            var actual = Get().Result;

            DomainAddress = address;
            AdminUsername = username;
            AdminPassword = pass;

            if (Validate(user, false).Result)
            {
                using (var tran = new TransactionScope())
                {
                    Insert().Wait();

                    var msg = globalization.GetString(globalization.DefaultLanguage, "DomainParametersCreated", new string[] { address, username, pass != null && pass.Trim().Length > 0 ? "created" : "not created" }).Result;

                    if (actual != null)
                    {
                        msg = globalization.GetString(globalization.DefaultLanguage, "DomainParametersChanged", new string[] { actual.DomainAddress, address, actual.AdminPassword != pass ? "yes" : "no" }).Result;
                    }

                    if (user != null)
                    {
                        new SystemRecord(msg, null, Common.EnumSystemFunctionality.Parameters, user.UserId, comments);
                    }

                    tran.Complete();
                }
            }
        }

        #endregion

        #region Attributes
        public string DomainAddress { get; set; }
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }
        #endregion

        #region User Code

        /// <summary>
        /// Name: Validate
        /// Description: It is a method that receives as a parameter user, newRecord and validates the DomainAddress and AdminPassword data.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        public async Task<bool> Validate(AuthenticatedUserDTO user, bool newRecord)
        {
            try
            {
                List<DomainError> erros = new List<DomainError>();

                var language = ObjectFactory.GetSingleton<IGlobalizationService>();

                if (DomainAddress == null || DomainAddress.Length == 0)
                {
                    erros.Add(new DomainError("DomainAddress", await language.GetString(user.CultureInfo, "DomainAddressInvalid")));
                }
               
                if (AdminPassword == null || AdminPassword.Length == 0)
                {
                    erros.Add(new DomainError("AdminPassword", await language.GetString(user.CultureInfo, "AdminPasswordInvalid")));
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
