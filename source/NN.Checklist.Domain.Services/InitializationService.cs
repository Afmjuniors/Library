using Microsoft.Extensions.Configuration;
using NN.Checklist.Domain.Configurations;
using NN.Checklist.Domain.Entities.Parameters;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using TDCore.Core.Logging;
using TDCore.Data.DatabaseHelper;
using TDCore.DependencyInjection;
using TDCore.Globalization;

namespace NN.Checklist.Domain.Services
{
    public static class InitializationService
    {
        /// <summary>
        /// Name: "InitializeApi" 
        /// Description: method starts the Api.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public static async Task InitializeApplication(bool checkDatabase, string logFile, string pathResources, string origns, string token, bool startMenuCache)
        {            
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            
            //Log initialization
            var logConfiguration = new LogConfiguration(logFile);
            TDCore.DependencyInjection.ObjectFactory.AddObjects(logConfiguration);

            //Audit Trail initialization
            var auditTrailConfiguration = new AuditTrailConfiguration();
            TDCore.DependencyInjection.ObjectFactory.AddObjects(auditTrailConfiguration);

            //Services initialization
            TDCore.DependencyInjection.ObjectFactory.AddObjects(new ServiceConfiguration(pathResources, origns, token));

            //Parameters intialization
            TDCore.DependencyInjection.ObjectFactory.AddObjects(new ParameterConfiguration(logConfiguration.ObjLogger));

            //Starts objects container
            TDCore.DependencyInjection.ObjectFactory.Initialize();

            //Starts Globalization service 
            Specifications.IGlobalizationService globalizationService = ObjectFactory.GetSingleton<Specifications.IGlobalizationService>();

            await globalizationService.StartGlobalization();

            globalizationService.DefaultLanguage = "pt-BR";

            var sql = DBParameter.Repository.Get();
            var domain = DomainParameter.Repository.Get();

#if DEBUG
            if (sql == null)
            {
                sql = new DBParameter(null, "Server=localhost;Database=checklist;User Id=sa;Password=zedasilva;", "checklist", "Configuração inicial");
            }

            if (domain == null)
            {
                domain = new DomainParameter(null, "TDD.Domain", "admin", "Novo@123", "Configuração inicial");
            }
#else
            if (sql == null)
            {
                sql = new DBParameter(null, "", "");
            }


#endif
            string tag = "Checklist";

            if (sql != null)
            {                
                try
                {
                    //Database creation and updating
                    var logger = TDCore.DependencyInjection.ObjectFactory.GetSingleton<ILog>();

                    var databaseHelper = new DatabaseBuilder(logger, enumDatabaseType.SqlServer, AppDomain.CurrentDomain.BaseDirectory, tag, sql.ConnectionStringSqlServer, sql.SqlServerSchema, version.ToString());
                    databaseHelper.Check();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + ex.StackTrace);
                    throw ex;
                }
                
                //Database objects initialization
                var databaseConfiguration = new DatabaseConfiguration(logConfiguration.ObjLogger, sql.ConnectionStringSqlServer);

                //Starts objects container
                TDCore.DependencyInjection.ObjectFactory.Initialize(databaseConfiguration.Objects);
            }           
        }

    }
}
