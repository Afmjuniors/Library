using Newtonsoft.Json;
using Library.Domain.Services.Specifications;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Library.Domain.Services
{
    public class CryptoService : ICryptoService
    {
        private byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8 }; 
                                                         
        private byte[] iv = { 1, 2, 3, 4, 5, 6, 7, 8 };

        private DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        /// <summary>
        /// Name: Decrypt
        /// Description: Decrypt byte json crypted
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public string Decrypt(string buffer)
        {
            try
            {
                byte[] data = Convert.FromBase64String(buffer);
                string result = "";
                using (var fs = new MemoryStream(data))
                using (var cryptoStream = new CryptoStream(fs, des.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    using(var sr = new StreamReader(cryptoStream))
                    {
                        result = sr.ReadToEnd();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Name: Encrypt
        /// Description: Generate a byte json crypted
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>

        public string Encrypt(object data)
        {
            try
            {
                byte[] result = null;
                string JSON_DATA = JsonConvert.SerializeObject(data);
                // Encryption
                using (var fs = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(fs, des.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(cryptoStream))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(JSON_DATA);
                        }
                        result = fs.ToArray();
                    }
                }
                return Convert.ToBase64String(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
