
using iTextSharp.text;
using Library.Domain.Common;
using Library.Domain.DTO;
using Library.Domain.DTO.Response;
using Library.Domain.Repositories.Specifications;
using Library.Domain.Services.Specifications;
using Newtonsoft.Json.Linq;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TDCore.Core;
using TDCore.Data;
using TDCore.DependencyInjection;
using TDCore.Domain;
using TDCore.Domain.Exceptions;
using static iTextSharp.text.pdf.AcroFields;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:12

#endregion

namespace Library.Domain.Entities
{
    public class Regulation : DomainBase<Regulation, IRegulationRepository<Regulation, System.Int64>, System.Int64>
    {

        #region Constructors

        /// <summary>
        /// Name: Organization
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public Regulation()
        {

        }

        public Regulation(AuthenticatedUserDTO user, EnumRules rule, string value, long organizationId )
        {
            OrganizationId = organizationId;
            RuleId = rule;
            Value = value;
            if (Validate(user,true).Result)
            {
                Insert().Wait();
            }

        }



        #endregion

        #region Attributes

        [AttributeDescriptor("Id", true, EnumValueRanges.Positive)]
        public System.Int64 RegulationId { get; internal set; }

        [AttributeDescriptor("UpdatedAt", true)]
        public System.DateTime? UpdatedAt { get; set; }

        [AttributeDescriptor("OrganizationId", true)]
        public long OrganizationId { get; set; }
        [AttributeDescriptor("RuleId", true)]
        public EnumRules RuleId { get; set; }
        [AttributeDescriptor("Value", false)]
        public string Value { get; set; }





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

                if (!newRecord && RegulationId <= 0)
                {
                    errors.Add(new DomainError("RegulationId", await globalization.GetString(lang, "User001")));
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