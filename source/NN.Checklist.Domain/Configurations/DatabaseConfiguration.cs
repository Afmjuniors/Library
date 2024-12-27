using TDCore.Core.Logging;
using TDCore.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using NN.Checklist.Domain.Common;
using NN.Checklist.Domain.Entities.Parameters;
using NN.Checklist.Domain.Repositories.Specifications;

namespace NN.Checklist.Domain.Configurations
{
    public class DatabaseConfiguration: ObjectPoolConfiguration
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
                AddObject(CreateRepository("AdGroupPermissionRepository", "NN.Checklist.Domain.Repositories.AdGroupPermissionRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("AdGroupRepository", "NN.Checklist.Domain.Repositories.AdGroupRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));                    
                AddObject(CreateRepository("AdGroupUserRepository", "NN.Checklist.Domain.Repositories.AdGroupUserRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));                    
                AddObject(CreateRepository("CountryRepository", "NN.Checklist.Domain.Repositories.CountryRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("DatatypeRepository", "NN.Checklist.Domain.Repositories.DatatypeRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("LanguageRepository", "NN.Checklist.Domain.Repositories.LanguageRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("PermissionRepository", "NN.Checklist.Domain.Repositories.PermissionRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("SystemRecordRepository", "NN.Checklist.Domain.Repositories.SystemRecordRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("UserPhoneRepository", "NN.Checklist.Domain.Repositories.UserPhoneRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("UserRepository", "NN.Checklist.Domain.Repositories.UserRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));





                AddObject(CreateRepository("BlockVersionChecklistTemplateRepository", "NN.Checklist.Domain.Repositories.BlockVersionChecklistTemplateRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("CancelledItemVersionChecklistTemplateRepository", "NN.Checklist.Domain.Repositories.CancelledItemVersionChecklistTemplateRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("ChecklistRepository", "NN.Checklist.Domain.Repositories.ChecklistRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("ChecklistTemplateRepository", "NN.Checklist.Domain.Repositories.ChecklistTemplateRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("CommentCheclistRepository", "NN.Checklist.Domain.Repositories.CommentCheclistRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("DependencyBlockVersionChecklistTemplateRepository", "NN.Checklist.Domain.Repositories.DependencyBlockVersionChecklistTemplateRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("DependencyItemVersionChecklistTemplateRepository", "NN.Checklist.Domain.Repositories.DependencyItemVersionChecklistTemplateRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("FieldChecklistRepository", "NN.Checklist.Domain.Repositories.FieldChecklistRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("FieldVersionChecklistTemplateRepository", "NN.Checklist.Domain.Repositories.FieldVersionChecklistTemplateRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("ItemChecklistRepository", "NN.Checklist.Domain.Repositories.ItemChecklistRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("ItemVersionChecklistTemplateRepository", "NN.Checklist.Domain.Repositories.ItemVersionChecklistTemplateRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("OptionFieldVersionChecklistTemplateRepository", "NN.Checklist.Domain.Repositories.OptionFieldVersionChecklistTemplateRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("OptionItemChecklistRepository", "NN.Checklist.Domain.Repositories.OptionItemChecklistRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("OptionItemVersionChecklistTemplateRepository", "NN.Checklist.Domain.Repositories.OptionItemVersionChecklistTemplateRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));
                AddObject(CreateRepository("VersionChecklistTemplateRepository", "NN.Checklist.Domain.Repositories.VersionChecklistTemplateRepository, NN.Checklist.Domain.Repositories", conexaoBDSQLServer, logger));





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
