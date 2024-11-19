using NN.Checklist.Domain.Common;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Entities.Bases;
using NN.Checklist.Domain.Repositories.Specifications;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using TDCore.DependencyInjection;
using TDCore.Domain;
using TDCore.Domain.Exceptions;
using TDCore.Globalization;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:37

#endregion

namespace NN.Checklist.Domain.Entities
{
    public class User : DomainBaseSRM<User, IUserRepository<User, System.Int64>, System.Int64>
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
        /// Description: Constructor method that receives as parameter datetimeDeactivate, deactivated, initials, languageId and does a validation, if true, is inserted into the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public User(AuthenticatedUserDTO user, System.DateTime? datetimeDeactivate, System.Boolean deactivated, System.String initials, System.Int32? languageId)
        {
            DatetimeDeactivate = datetimeDeactivate; 
            Deactivated = deactivated; 
            Initials = initials; 
            LanguageId = languageId; 

            if (Validate(user, true).Result)
            {
                Insert().Wait();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("Id", true, EnumValueRanges.Positive)]
        public System.Int64 UserId { get; internal set; }

        [AttributeDescriptor("Datetime deactivate", false)] 
        public System.DateTime? DatetimeDeactivate { get; set; }

        [AttributeDescriptor("Deactivated?", true)] 
        public System.Boolean Deactivated { get; set; }

        [AttributeDescriptor("Initials", true)] 
        public System.String Initials { get; set; }

        [AttributeDescriptor("Language Id", false)] 
        public System.Int32? LanguageId { get; set; }

        public Language Language { get => GetManyToOneData<Language>().Result; }

        public bool Active { get => !Deactivated;  }
        #endregion

        #region User Code

        /// <summary>
        /// Name: Activate
        /// Description: Non-return method that receives user as parameter, activates and activates the user if activated and updates in the database and informs with a message or status.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        public async Task Activate(AuthenticatedUserDTO user, bool active)
        {
            try
            {
                Deactivated = active;

                using (var tran = new TransactionScope())
                {
                    await Update();

                    tran.Complete();
                }

                var globalization = ObjectFactory.GetSingleton<Services.Specifications.IGlobalizationService>();
                string lang = user.CultureInfo;

                var msg = await globalization.GetString(globalization.DefaultLanguage, "UserActivated", new string[] { Initials });
                if (Deactivated)
                {
                    msg = await globalization.GetString(globalization.DefaultLanguage, "UserDeactivated", new string[] { Initials });
                }

                new SystemRecord(msg, UserId, EnumSystemFunctionality.Users, user.UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

                var globalization = ObjectFactory.GetSingleton<Services.Specifications.IGlobalizationService>();
                string lang = null;

                if (user != null)
                {
                    lang = user.CultureInfo;
                }

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

        /// <summary>
        /// Name: Validate
        /// Description: Method that receives as parameter user, newRecord and validates if user is different from null, if yes, lang receives user.CultureInfo.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task UpdateLanguage(long actionUserId, int languageId)
        {
            try
            {
                var actual = this;

                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

                LanguageId = languageId;


                await Update();

            }
            catch (DomainException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DomainException("ErrorUpdatingUserProfilePermissions", ex);
            }
        }

        #endregion
    }
}