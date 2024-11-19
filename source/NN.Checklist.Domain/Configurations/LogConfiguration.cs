using TDCore.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;

namespace NN.Checklist.Domain.Configurations
{
    public class LogConfiguration : ObjectPoolConfiguration
    {
        /// <summary>
        /// Name: LogConfiguration
        /// Description: It is the constructor method of LogConfiguration that receives as a configuration parameter and receives the CreateObjects method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public LogConfiguration(string logFile) : base(null)
        {
            CreateObjects(logFile);
        }

        public TDCore.DependencyInjection.Object ObjLogger { get; set; }

        /// <summary>
        /// Name: CreateObjects
        /// Description: The CreateObjects method creates a log object.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public void CreateObjects(string logFile)
        {
            try
            {
                var objLogger = new TDCore.DependencyInjection.Object();
                objLogger.ObjectID = "Log";
                objLogger.IsSingleton = true;
                objLogger.Type = "TDCore.Logging.Log, TDCore.Logging";
                objLogger.AddProperty("FileName", logFile);

                ObjLogger = objLogger;

                AddObject(objLogger);
            }
            catch (Exception ex)
            {
                throw new Exception("Log objects configuration error", ex);
            }
        }


    }
}
