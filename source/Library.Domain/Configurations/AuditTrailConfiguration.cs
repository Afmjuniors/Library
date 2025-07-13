using TDCore.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace Library.Domain.Configurations
{
    public class AuditTrailConfiguration : ObjectPoolConfiguration
    {
        /// <summary>
        /// Name: ServiceConfiguration
        /// Description: It is a constructor method that takes configuration as a parameter and uses the CreateObjects method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public AuditTrailConfiguration() : base(null)
        {
            CreateObjects();
        }

        /// <summary>
        /// Name: CreateObjects
        /// Description: Adds objects with AddObject, receives a GetSection in the origins variable, does not check if it is null or not, then creates the object by adding the properties or throws an exception.
        /// Created by: wazc Programa Novo 2022-09-08  
        /// </summary>
        public void CreateObjects()
        {
            try
            {
                AddObject("AuditTrailService", true, "Library.Domain.Services.AuditTrailService, Library.Domain.Services");
            }
            catch (Exception ex)
            {
                throw new Exception("Services objects configuration error", ex);
            }
        }
    }
}
