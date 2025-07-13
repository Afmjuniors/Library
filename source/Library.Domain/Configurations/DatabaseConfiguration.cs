using TDCore.Core.Logging;
using TDCore.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace LibraryLibrary.Domain.Configurations
{
    public class DatabaseConfiguration : ObjectPoolConfiguration
    {
        /// <summary>
        /// Name: DatabaseConfiguration
        /// Description: It is a constructor method that receives as parameter configuration, logger, connectionString, messageQueue and in its body receives the CreateObjects method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public DatabaseConfiguration(TDCore.DependencyInjection.Object logger, string connectionString) : base(null)
        {
            CreateObjects(logger, connectionString);
        }

        /// <summary>
        /// Name: CreateObjects
        /// Description: It takes the logger, connectionString and messageQueue as parameters. Check that the user is logged in, connects to the database and issues a message, and creates the object. After that, it adds the CreateRepository method to the objects to create the repositories.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// Updated by: wazc CR0838177 2023-09-01 
        /// Description: Changed to add release areas.
        /// </summary>
        public void CreateObjects(TDCore.DependencyInjection.Object logger, string connectionString)
        {
            try
            {
                TDCore.DependencyInjection.Object conexaoBDSQLServer = new TDCore.DependencyInjection.Object();
                conexaoBDSQLServer.ObjectID = "ConexaoBDOracle";
                conexaoBDSQLServer.IsSingleton = true;
                conexaoBDSQLServer.Type = "TDCore.Data.SqlServer.Connection, TDCore.Data.SqlServer";
                conexaoBDSQLServer.AddProperty("ConnectionString", connectionString);
                AddObject(conexaoBDSQLServer);

                //Repositórios
                AddObject(CreateRepository("AdGroupPermissionRepository", "Library.Domain.Repositories.AdGroupPermissionRepository, Library.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("AdGroupRepository", "Library.Domain.Repositories.AdGroupRepository, Library.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("AdGroupUserRepository", "Library.Domain.Repositories.AdGroupUserRepository, Library.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("AreaRepository", "Library.Domain.Repositories.AreaRepository, Library.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("CountryRepository", "Library.Domain.Repositories.CountryRepository, Library.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("DatatypeRepository", "Library.Domain.Repositories.DatatypeRepository, Library.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("LanguageRepository", "Library.Domain.Repositories.LanguageRepository, Library.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("PermissionRepository", "Library.Domain.Repositories.PermissionRepository, Library.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("ProcessRepository", "Library.Domain.Repositories.ProcessRepository, Library.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("SystemRecordRepository", "Library.Domain.Repositories.SystemRecordRepository, Library.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("UserPhoneRepository", "Library.Domain.Repositories.UserPhoneRepository, Library.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("UserRepository", "Library.Domain.Repositories.UserRepository, Library.Domain.Repositories", conexaoBDSQLServer, logger));

            }
            catch (Exception ex)
            {
                throw new Exception("Database objects configuration error", ex);
            }
        }

        /// <summary>
        /// Name: CreateRepository
        /// Description: It takes the name, type, db and logger as parameters and creates the repository.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        private TDCore.DependencyInjection.Object CreateRepository(string name, string type, TDCore.DependencyInjection.Object db, TDCore.DependencyInjection.Object logger)
        {
            var obj = new TDCore.DependencyInjection.Object();
            obj.ObjectID = name;
            obj.IsSingleton = true;
            obj.Type = type;
            obj.AddProperty("Connection", db);
            obj.AddProperty("Logger", logger);

            return obj;
        }


    }
}
