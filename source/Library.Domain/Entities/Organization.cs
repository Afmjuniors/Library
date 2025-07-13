
using Library.Domain.Common;
using Library.Domain.DTO;
using Library.Domain.DTO.Response;
using Library.Domain.Repositories.Specifications;
using Library.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
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
using static iTextSharp.text.pdf.events.IndexEvents;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:11

#endregion

namespace Library.Domain.Entities
{
    public class Organization : DomainBase<Organization, IOrganizationRepository<Organization, System.Int64>, System.Int64>
    {

        #region Constructors

        /// <summary>
        /// Name: Organization
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public Organization()
        {

        }

        /// <summary>
        /// Name: Organization
        /// Description: Constructor method that receives as parameter datetimeDeactivate, deactivated, initials, languageId and does a validation, if true, is inserted into the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public Organization(AuthenticatedUserDTO user, string name, string description, string image)
        {
            CreatedBy = user.UserId;
            CreatedAt = DateTime.Now;
            Name = name;
            Description = description;
            Image = image;

            if (Validate(user, true).Result)
            {
                Insert().Wait();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("Id", true, EnumValueRanges.Positive)]
        public System.Int64 OrganizationId { get; internal set; }

        [AttributeDescriptor("Created_at", true)]
        public System.DateTime? CreatedAt { get; set; }

        [AttributeDescriptor("Name", true)]
        public System.String Name { get; set; }
        [AttributeDescriptor("CreatedBy", true)]
        public long CreatedBy { get; set; }
        [AttributeDescriptor("UpdatedBy", false)]
        public long UpdatedBy { get; set; }
        [AttributeDescriptor("UpdatedAt", false)]
        public System.DateTime? UpdatedAt { get; set; }

        [AttributeDescriptor("Description", false)]
        public System.String Description { get; set; }

        [AttributeDescriptor("Image", false)]
        public System.String Image { get; set; }






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

                if (!newRecord && OrganizationId <= 0)
                {
                    errors.Add(new DomainError("OrganizationId", await globalization.GetString(lang, "User001")));
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