using TDCore.Core.Logging;
using NN.Checklist.Domain.Entities.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Mail;

namespace NN.Checklist.Domain.Entities
{
    public class EmailSender
    {
        public static ILog Logger { get; set; }
        /// <summary>
        /// Name: ZipAttachment
        /// Description: Method that takes as parameter email, tmpPath, attach and compresses the attachment and decompresses it.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public static string ZipAttachment(string email, string tmpPath, string[] attach)
        {
            try
            {
                List<string> paths = new List<string>();
                string date = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
                string compactPath = $"{tmpPath}\\{date}_{Path.GetFileName(email)}";
                string zipFileName = $"{tmpPath}\\{date}_{Path.GetFileName(email)}.zip";
                DirectoryInfo di = new DirectoryInfo(compactPath);

                if (!di.Exists)
                {
                    di.Create();
                }

                foreach (var item in attach)
                    System.IO.File.Copy(item, $"{compactPath}\\{Path.GetFileName(item)}", true);
                ZipFile.CreateFromDirectory(compactPath, zipFileName);

                Directory.Delete(compactPath, true);
                return zipFileName;
            }
            catch (Exception ex)
            {
                Logger.Log(LogType.Error, ex);
                return null;
            }
        }

        /// <summary>
        /// Name: SendMail
        /// Description: Method that receives as parameter email, subject, body, tmpPath, attachments for sending email.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public static void SendMail(string email, string subject, string body, string tmpPath, string[] attachments)
        {
            try
            {
                var pars = MailParameter.Repository.Get();

                var fromAddress = new MailAddress(pars.SMTPFromAddress); 
                var toAddress = new MailAddress(email);
                string fromPassword = pars.SMTPPassword; 

                var zipPaths = "";
                if (attachments != null)
                {
                   zipPaths = ZipAttachment(email, tmpPath, attachments);
                }

                using (var message = new MailMessage(fromAddress, toAddress))
                {
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;

                    if (attachments != null)
                    {
                        message.Attachments.Add(new Attachment(zipPaths));
                    }

                    using (var smtp = new SmtpClient())
                    {
                        smtp.Host = pars.SMTPServer; 
                        smtp.Port = pars.SMTPPort;
                        smtp.EnableSsl = pars.SMTPEnabledSSL; 
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Timeout = 100000;

                        smtp.UseDefaultCredentials = false;

                        if (!string.IsNullOrEmpty(fromPassword))
                        {
                           
                            smtp.Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword);
                        }                       

                        smtp.Send(message);
                    }
                }

                if (attachments != null)
                {
                    System.IO.File.Delete(zipPaths);                    
                }

                if (attachments != null)
                {
                    foreach (var item in attachments)
                    {
                        System.IO.File.Delete(item);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Logger.Log(LogType.Error, ex);
                throw ex;
            }
        }
    }
}
