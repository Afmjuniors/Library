using TDCore.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Configurations
{
    public class ParameterConfiguration : ObjectPoolConfiguration
    {
        /// <summary>
        /// Name: ParameterConfiguration
        /// Description: It is a constructor method that receives a logger as a configuration parameter and uses the CreateObjects method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public ParameterConfiguration(TDCore.DependencyInjection.Object logger) : base(null)
        {
            CreateObjects(logger);
        }

        /// <summary>
        /// Name: CreateObjects
        /// Description: The CreateObjects method creates the parameters of objects.
        /// Created by: wazc Programa Novo 2022-09-08
        /// Updated by: wazc CR0838177 2023-09-01 
        /// Description: Changed to add watchdog alarms. 
        /// </summary>
        public void CreateObjects(TDCore.DependencyInjection.Object logger)
        {
            try
            {
                var obj = new TDCore.DependencyInjection.Object();
                obj.ObjectID = "DBParameterRepository";
                obj.IsSingleton = true;
                obj.Type = "Library.Domain.Repositories.TextFile.DBParameterRepository, Library.Domain.Repositories.TextFile";
                obj.AddProperty("Logger", logger);
                AddObject(obj);

                var objPolicy = new TDCore.DependencyInjection.Object();
                objPolicy.ObjectID = "PolicyParameterRepository";
                objPolicy.IsSingleton = true;
                objPolicy.Type = "Library.Domain.Repositories.TextFile.PolicyParameterRepository, Library.Domain.Repositories.TextFile";
                objPolicy.AddProperty("Logger", logger);
                AddObject(objPolicy);

                var objMail = new TDCore.DependencyInjection.Object();
                objMail.ObjectID = "MailParameterRepository";
                objMail.IsSingleton = true;
                objMail.Type = "Library.Domain.Repositories.TextFile.MailParameterRepository, Library.Domain.Repositories.TextFile";
                objMail.AddProperty("Logger", logger);
                AddObject(objMail);


            }
            catch (Exception ex)
            {
                throw new Exception("Database objects configuration error", ex);
            }
        }
    }
}
