using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TDCore.DependencyInjection;

namespace NN.Checklist.Domain.Services
{
    public class MailService : ObjectBase, IMailService
    {
        /// <summary>
        /// Name: "SendExportedAlertFromMail" 
        /// Description: method receives the parameters "user", "filePath" and "fileName" and sends an alert from export to the email.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task SendExportedAlertFromMail(AuthenticatedUserDTO user, string filePath, string fileName)
        {
            try
            {                
                new Thread(() =>
                {
                    try
                    {
                        var global = ObjectFactory.GetSingleton<IGlobalizationService>();
                        var messageSubject = global.GetString(user.CultureInfo, "SendExportedAlertMailSubject").Result;
                        var messageBody = global.GetString(user.CultureInfo, "SendExportedAlertMailBody").Result;
                        var email = user.UserAD+"@novonordisk.com";
                        EmailSender.Logger =  Logger;
                        EmailSender.SendMail(email, messageSubject, messageBody, filePath, new string[] { fileName });
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(TDCore.Core.Logging.LogType.Error, ex);
                    }
                }).Start();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Name: "SendExportedAlertFromMail" 
        /// Description: method receives the parameters "user", "filePath", "fileName" and "searchingParameters" and sends an alert from export to the email.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task SendExportedAlertFromMail(AuthenticatedUserDTO user, string email, string filePath, string fileName, string searchingParameters)
        {
            try
            {   
                new Thread(() =>
                {
                    try
                    {
                        var global = ObjectFactory.GetSingleton<IGlobalizationService>();
                        var messageSubject = global.GetString(user.CultureInfo, "SendExportedAlertMailSubject").Result;
                        var messageBody = global.GetString(user.CultureInfo, "MailText").Result;
                        EmailSender.Logger = Logger;
                        EmailSender.SendMail(email, messageSubject, messageBody, filePath, new string[] { fileName });
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }).Start();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
