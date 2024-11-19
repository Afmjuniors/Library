    using TDCore.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.Configurations
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
                obj.Type = "NN.Checklist.Domain.Repositories.TextFile.DBParameterRepository, NN.Checklist.Domain.Repositories.TextFile";
                obj.AddProperty("Logger", logger);
                AddObject(obj);

                var objDomain = new TDCore.DependencyInjection.Object();
                objDomain.ObjectID = "DomainParameterRepository";
                objDomain.IsSingleton = true;
                objDomain.Type = "NN.Checklist.Domain.Repositories.TextFile.DomainParameterRepository, NN.Checklist.Domain.Repositories.TextFile";
                objDomain.AddProperty("Logger", logger);
                AddObject(objDomain);

                var objPolicy = new TDCore.DependencyInjection.Object();
                objPolicy.ObjectID = "PolicyParameterRepository";
                objPolicy.IsSingleton = true;
                objPolicy.Type = "NN.Checklist.Domain.Repositories.TextFile.PolicyParameterRepository, NN.Checklist.Domain.Repositories.TextFile";
                objPolicy.AddProperty("Logger", logger);
                AddObject(objPolicy);
                
                var objMail = new TDCore.DependencyInjection.Object();
                objMail.ObjectID = "MailParameterRepository";
                objMail.IsSingleton = true;
                objMail.Type = "NN.Checklist.Domain.Repositories.TextFile.MailParameterRepository, NN.Checklist.Domain.Repositories.TextFile";
                objMail.AddProperty("Logger", logger);
                AddObject(objMail);

                var objMessaging = new TDCore.DependencyInjection.Object();
                objMessaging.ObjectID = "MessagingParameterRepository";
                objMessaging.IsSingleton = true;
                objMessaging.Type = "NN.Checklist.Domain.Repositories.TextFile.MessagingParameterRepository, NN.Checklist.Domain.Repositories.TextFile";
                objMessaging.AddProperty("Logger", logger);
                AddObject(objMessaging);
            }
            catch (Exception ex)
            {
                throw new Exception("Database objects configuration error", ex);
            }
        }
    }
}
