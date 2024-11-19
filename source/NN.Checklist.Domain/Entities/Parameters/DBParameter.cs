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
    public class DBParameter : TextFileDomainBase<DBParameter, IDBParameterRepository<DBParameter>>
    {

        #region Constructors
        /// <summary>
        /// Name: DBParameter
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public DBParameter()
        {

        }

        /// <summary>
        /// Name: DBParameter
        /// Description: Constructor method that receives as parameter user, connectionString, schema, comments and makes a connection to the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        public DBParameter(AuthenticatedUserDTO user, string connectionString, string schema, string comments)
        {
            var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

            var actual = Get().Result;

            ConnectionStringSqlServer = connectionString;
            SqlServerSchema = schema;

            if (Validate(user, false).Result)
            {
                using (var tran = new TransactionScope())
                {
                    Insert().Wait();

                    var msg = globalization.GetString(globalization.DefaultLanguage, "DBParametersCreated", new string[] { connectionString, schema }).Result;

                    if (actual != null)
                    {
                        msg = globalization.GetString(globalization.DefaultLanguage, "DBParametersChanged", new string[] { actual.ConnectionStringSqlServer, connectionString, actual.SqlServerSchema, SqlServerSchema }).Result;
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
        public string ConnectionStringSqlServer { get; set; }
        public string SqlServerSchema { get; set; }
        #endregion

        #region User Code

        /// <summary>
        /// Name: Validate
        /// Description: Method that takes the newRecord user as a parameter and validates the connection with the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        public async Task<bool> Validate(AuthenticatedUserDTO user, bool newRecord)
        {
            try
            {
                List<DomainError> erros = new List<DomainError>();

                var language = ObjectFactory.GetSingleton<IGlobalizationService>();

                if (ConnectionStringSqlServer == null || ConnectionStringSqlServer.Length == 0)
                {
                    erros.Add(new DomainError("ConnectionString", await language.GetString(user.CultureInfo, new string[] { "ConnectionString" })));
                }

                if (SqlServerSchema == null || SqlServerSchema.Length == 0)
                {
                   erros.Add(new DomainError("Schema", "Schema não foi informado."));
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
