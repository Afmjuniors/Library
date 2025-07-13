using TDCore.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using Newtonsoft.Json.Linq;
using iTextSharp.text.pdf;

namespace Library.Domain.Configurations
{
    public class ServiceConfiguration : ObjectPoolConfiguration
    {
        /// <summary>
        /// Name: ServiceConfiguration
        /// Description: It is a constructor method that takes configuration as a parameter and uses the CreateObjects method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public ServiceConfiguration(string pathResources, string origins, string token) : base(null)
        {
            CreateObjects(pathResources, origins, token);
        }

        /// <summary>
        /// Name: CreateObjects
        /// Description: Adds objects with AddObject, receives a GetSection in the origins variable, does not check if it is null or not, then creates the object by adding the properties or throws an exception.
        /// Created by: wazc Programa Novo 2022-09-08  
        /// </summary>
        public void CreateObjects(string pathResources, string origins, string token)
        {
            try
            {
                AddObject("AccessControlService", true, "Library.Domain.Services.AccessControlService, Library.Domain.Services");
                AddObject("PermissionService", true, "Library.Domain.Services.PermissionService, Library.Domain.Services");
                AddObject("ParameterService", true, "Library.Domain.Services.ParameterService, Library.Domain.Services");
                AddObject("ApplicationService", true, "Library.Domain.Services.ApplicationService, Library.Domain.Services");
                AddObject("MailService", true, "Library.Domain.Services.MailService, Library.Domain.Services");
                AddObject("CryptoService", true, "Library.Domain.Services.CryptoService, Library.Domain.Services");
                AddObject("CollectorService", true, "Library.Domain.Services.CollectorService, Library.Domain.Services");

                var globalizationService = new TDCore.DependencyInjection.Object();
                globalizationService.ObjectID = "GlobalizationService";
                globalizationService.IsSingleton = true;
                globalizationService.Type = "Library.Domain.Services.GlobalizationService, Library.Domain.Services";
                globalizationService.AddProperty("PathResources", pathResources);
                globalizationService.AddProperty("DefaultLanguage", "pt-BR");

                AddObject(globalizationService);

                if (origins != null)
                {
                    var securityService = new TDCore.DependencyInjection.Object();
                    securityService.ObjectID = "SecurityService";
                    securityService.IsSingleton = true;
                    securityService.Type = "TDCore.Web.Security.SecurityService, TDCore.Web";
                    securityService.AddProperty("PrivateKey", token);
                    securityService.AddProperty("Issuer", origins);
                    securityService.AddProperty("Audience", origins);

                    AddObject(securityService);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Services objects configuration error", ex);
            }
        }
    }
}
