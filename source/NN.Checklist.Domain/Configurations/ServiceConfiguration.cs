using TDCore.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using Newtonsoft.Json.Linq;
using iTextSharp.text.pdf;

namespace NN.Checklist.Domain.Configurations
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
                AddObject("AccessControlService", true, "NN.Checklist.Domain.Services.AccessControlService, NN.Checklist.Domain.Domain.Services");
                AddObject("PermissionService", true, "NN.Checklist.Domain.Services.PermissionService, NN.Checklist.Domain.Services");
                AddObject("ParameterService", true, "NN.Checklist.Domain.Services.ParameterService, NN.Checklist.Domain.Services");
                AddObject("ApplicationService", true, "NN.Checklist.Domain.Services.ApplicationService, NN.Checklist.Domain.Services");
                AddObject("MailService", true, "NN.Checklist.Domain.Services.MailService, NN.Checklist.Domain.Services");
                AddObject("CryptoService", true, "NN.Checklist.Domain.Services.CryptoService, NN.Checklist.Domain.Services");
                AddObject("CollectorService", true, "NN.Checklist.Domain.Services.CollectorService, NN.Checklist.Domain.Services");

                var globalizationService = new TDCore.DependencyInjection.Object();
                globalizationService.ObjectID = "GlobalizationService";
                globalizationService.IsSingleton = true;
                globalizationService.Type = "NN.Checklist.Domain.Services.GlobalizationService, NN.Checklist.Domain.Services";
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
