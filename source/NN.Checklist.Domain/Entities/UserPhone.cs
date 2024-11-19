using NN.Checklist.Domain.Common;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Entities.Bases;
using NN.Checklist.Domain.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TDCore.DependencyInjection;
using TDCore.Domain;
using TDCore.Domain.Exceptions;
using TDCore.Globalization;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:38

#endregion

namespace NN.Checklist.Domain.Entities
{
    public class UserPhone : DomainBaseSRM<UserPhone, IUserPhoneRepository<UserPhone, System.Int64>, System.Int64>
    {

        #region Constructors

        /// <summary>
        /// Name: UserPhone
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public UserPhone()
        {

        }

        /// <summary>
        /// Name: UserPhone
        /// Description: Constructor method that receives as parameter user, countryId, number, userId and does a validation, if true, is inserted into the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        public UserPhone(AuthenticatedUserDTO user, System.Int32 countryId, System.String number, System.Int64 userId)
        {
            CountryId = countryId; 
            Number = number; 
            UserId = userId;

            if (Validate(user, true).Result)
            {
                Insert().Wait() ;

                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
                string lang = user.CultureInfo;

                string phoneUser = $"{User.Initials} - {Country.PrefixNumber}-{number}";

                var msg = globalization.GetString(globalization.DefaultLanguage, "NewUserPhone", new string[] { phoneUser }).Result;
                new SystemRecord(msg, UserPhoneId, EnumSystemFunctionality.Users, user.UserId);
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("User Phone Id", true, EnumValueRanges.Positive)]
        public System.Int64 UserPhoneId { get; set; }

        [AttributeDescriptor("Country Id", true)] 
        public System.Int32 CountryId { get; set; }

        [AttributeDescriptor("Number", true)] 
        public System.String Number { get; set; }

        [AttributeDescriptor("User Id", true)] 
        public System.Int64 UserId { get; set; }

        public Country Country { get => GetManyToOneData<Country>().Result; }

        public User User { get => GetManyToOneData<User>().Result; }



        #endregion

        #region User Code

        /// <summary>
        /// Name: Validate
        /// Description: Method that receives as parameter user, newRecord and validates if user is different from null, if yes, lang receives user.CultureInfo.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<bool> Validate(AuthenticatedUserDTO user, bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
                string lang = null;

                if (user != null)
                {
                    lang = user.CultureInfo;
                }

                List<DomainError> errors = new List<DomainError>();

                if (!newRecord && UserPhoneId <= 0)
                {
                    errors.Add(new DomainError("UserPhoneId", await globalization.GetString(lang, "UserPhone001")));
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

        #endregion
    }
}