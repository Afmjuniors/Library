using Library.Domain.Common;
using Library.Domain.DTO.Response;
using Library.Domain.Entities.Bases;
using Library.Domain.Repositories.Specifications;
using Library.Domain.Services.Specifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using TDCore.Core;
using TDCore.DependencyInjection;
using TDCore.Domain;
using TDCore.Domain.Exceptions;
using TDCore.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:37

#endregion

namespace Library.Domain.Entities
{
    public class User : DomainBase<User, IUserRepository<User, System.Int64>, System.Int64>
    {

        #region Constructors

        /// <summary>
        /// Name: User
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public User()
        {

        }
        /// <summary>
        /// Name: User
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public User(string email, string name, string password)
        {
            var cryptoService = ObjectFactory.GetSingleton<ICryptoService>();
            LanguageId = 1;
            CreatedAt = DateTime.Now;
            Email = email;
            Name = name;
            Password = cryptoService.Encrypt(password);
            UserStatusId = EnumUserStatus.Active;

            if (Validate(true).Result)
            {
                Insert().Wait();
            }
        }
        /// <summary>
        /// Name: User
        /// Description: Constructor method that receives as parameter datetimeDeactivate, deactivated, initials, languageId and does a validation, if true, is inserted into the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public User(int languageId, DateTime? birthday, string email, string name, string phone, string adress, string adcionalInfo, string password, string image)
        {
            var cryptoService = ObjectFactory.GetSingleton<ICryptoService>();
            LanguageId = languageId;
            CreatedAt = DateTime.Now;
            BirthDay = birthday;
            Email = email;
            Name = name;
            Phone = phone;
            Address = adress;
            AdditionalInfo = adcionalInfo;
            Password = cryptoService.Encrypt(password);
            Image = image;
            UserStatusId = EnumUserStatus.Active;

            if (Validate(true).Result)
            {
                Insert().Wait();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("Id", true, EnumValueRanges.Positive)]
        public System.Int64 UserId { get; internal set; }

        [AttributeDescriptor("Created_at", true)]
        public System.DateTime? CreatedAt { get; set; }
        [AttributeDescriptor("BirthDay", false)]
        public System.DateTime? BirthDay { get; set; }

        [AttributeDescriptor("Email", true)]
        public System.String Email { get; set; }
        [AttributeDescriptor("Name", true)]
        public System.String Name { get; set; }

        [AttributeDescriptor("Phone", false)]
        public System.String Phone { get; set; }
        [AttributeDescriptor("Address", false)]
        public System.String Address { get; set; }
        [AttributeDescriptor("AdditionalInfo", false)]
        public System.String AdditionalInfo { get; set; }
        [AttributeDescriptor("LanguageId", false)]
        public int LanguageId { get; set; }
        [AttributeDescriptor("Password", true)]
        public System.String Password { get; set; }
        [AttributeDescriptor("Image", false)]
        public System.String Image { get; set; }
        [AttributeDescriptor("UserStatusId", false)]
        public EnumUserStatus UserStatusId { get; set; }

        public Language Language { get => GetManyToOneData<Language>().Result; }

        #endregion

        #region User Code




        /// <summary>
        /// Name: Validate
        /// Description: Method that receives as parameter user, newRecord and validates if user is different from null, if yes, lang receives user.CultureInfo.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                var globalization = ObjectFactory.GetSingleton<Services.Specifications.IGlobalizationService>();
                string lang = globalization.DefaultLanguage;


                List<DomainError> errors = new List<DomainError>();

                if (!newRecord && UserId <= 0)
                {
                    errors.Add(new DomainError("UserId", await globalization.GetString(lang, "User001")));
                }

                if (errors.Count > 0)
                {
                    throw new DomainException(await globalization.GetString(lang, "DataDomainError"), errors);
                }

                return true;
            }
            catch (DomainException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool AuthenticatePassword(string password)
        {
            try
            {
                var cryptoService = ObjectFactory.GetSingleton<ICryptoService>();

                var passDecode = cryptoService.Decrypt(Password);

                passDecode = JsonConvert.DeserializeObject<string>(passDecode);
                if (passDecode == password)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        #endregion
    }
}