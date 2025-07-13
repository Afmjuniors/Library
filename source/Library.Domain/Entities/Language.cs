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
//Generated: 14/2021 15:09:37

#endregion

namespace Library.Domain.Entities
{
    public class Language : DomainBase<Language, ILanguageRepository<Language, System.Int32>, System.Int32>
    {

        #region Constructors

        /// <summary>
        /// Name:Language
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public Language()
        {

        }

        /// <summary>
        /// Name: Language
        /// Description: Constructor method that receives as a parameter code, countryId, name and validates if it is true.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public Language(AuthenticatedUserDTO user, System.String code, System.Int32 countryId, System.String name)
        {
            Code = code; 
            CountryId = countryId; 
            Name = name; 

            if (Validate(user, true).Result)
            {
                Insert().Wait();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("Language Id", true, EnumValueRanges.Positive)]
        public System.Int32 LanguageId { get; internal set; }

        [AttributeDescriptor("Code", true)] 
        public System.String Code { get; set; }

        [AttributeDescriptor("Country Id", true)] 
        public System.Int32 CountryId { get; set; }

        [AttributeDescriptor("Name", true)] 
        public System.String Name { get; set; }

        public Country Country { get => GetManyToOneData<Country>().Result; }



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
                    errors.Add(new DomainError("LanguageId", await globalization.GetString(lang, "Language001")));
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