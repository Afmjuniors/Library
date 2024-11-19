using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Entities.Bases;
using NN.Checklist.Domain.Repositories.Specifications;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TDCore.DependencyInjection;
using TDCore.Domain;
using TDCore.Domain.Exceptions;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 10/2021 12:16:49

#endregion

namespace NN.Checklist.Domain.Entities
{
    public class Datatype : DomainBaseSRM<Datatype, IDatatypeRepository<Datatype, System.Int32>, System.Int32>
    {

        #region Constructors
        /// <summary>
        /// Name: Datatype
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public Datatype()
        {

        }

        /// <summary>
        /// Name: Datatype
        /// Description: Constructor method that receives as a parameter name, typename and validates if it is true.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public Datatype(AuthenticatedUserDTO user, System.String name, System.String typename)
        {
            Name = name; 
            Typename = typename; 

            if (Validate(user, true).Result)
            {
                Insert().Wait();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("Datatype Id", true, EnumValueRanges.Positive)]
        public System.Int32 DatatypeId { get; set; }

        [AttributeDescriptor("Name", true)] 
        public System.String Name { get; set; }

        [AttributeDescriptor("Typename", true)] 
        public System.String Typename { get; set; }



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

                if (!newRecord && DatatypeId <= 0)
                {
                    errors.Add(new DomainError("DataTypeId", await globalization.GetString(lang, "DataType001")));
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