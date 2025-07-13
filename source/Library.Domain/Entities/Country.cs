using Library.Domain.DTO.Response;
using Library.Domain.Entities.Bases;
using Library.Domain.Repositories.Specifications;
using Library.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TDCore.DependencyInjection;
using TDCore.Domain;
using TDCore.Domain.Exceptions;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 10/2021 12:16:47

#endregion

namespace Library.Domain.Entities
{
    public class Country : DomainBase<Country, ICountryRepository<Country, System.Int32>, System.Int32>
    {

        #region Constructors
        /// <summary>
        /// Name: Country
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public Country()
        {

        }

        /// <summary>
        /// Name: Country
        /// Description: Constructor method that receives as parameter name, prefixNumber and validates if it is true.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public Country(AuthenticatedUserDTO user, System.String name, System.Int32 prefixNumber)
        {
            Name = name; 
            PrefixNumber = prefixNumber; 

            if (Validate(user, true).Result)
            {
                Insert().Wait();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("Country Id", true, EnumValueRanges.Positive)]
        public System.Int32 CountryId { get; internal set; }

        [AttributeDescriptor("Name", true)] 
        public System.String Name { get; set; }

        [AttributeDescriptor("Prefix Number", true)] 
        public System.Int32 PrefixNumber { get; set; }



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

                if (!newRecord && CountryId <= 0)
                {
                    errors.Add(new DomainError("CountryId", await globalization.GetString(lang, "Country001")));
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