using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.Entities.Bases
{
    public class FileBase
    {

        public string PathName { get; set; }

        /// <summary>
        /// Name: FileBase
        /// Description: It is a constructor method that receives name as a parameter, PathName receives name, or checks if it is null or empty, if PathName receives a message.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public FileBase(string name)
        {
            PathName = $"{name}";
            if (string.IsNullOrEmpty(name))
            {
                PathName = "exported_data";
            }
        }

        /// <summary>
        /// Name: Incluir
        /// Description: includes a new line in file.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        
        public void Incluir<T>(string tag, DateTime timestamp, string sourceId, T value) where T : struct
        {
            try
            {
                //before your loop
                var csv = new StringBuilder();

                var newLine = string.Format($"{tag};{timestamp.ToString("yyyy-MM-dd HH:mm:ss")};{value};");
                csv.AppendLine(newLine);

                //after your loop
                File.AppendAllText($"{AppDomain.CurrentDomain.BaseDirectory}/{PathName}_{tag.Replace(".", "_")}.csv", csv.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}