using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Entities.Bases;
using NN.Checklist.Domain.Repositories.Specifications;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TDCore.DependencyInjection;
using TDCore.Domain;
using TDCore.Domain.Exceptions;

namespace NN.Checklist.Domain.Entities
{
    public class AdGroupUser : DomainBaseSRM<AdGroupUser, IAdGroupUserRepository<AdGroupUser, System.Int64>, System.Int64>
    {
        #region Constructors
        /// <summary>
        /// Name: AdGroupUser
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public AdGroupUser()
        {

        }

        /// <summary>
        /// Name: AdGroupUser
        /// Description: Constructor method that receives as a parameter adGroupId, userId and checks if the data has been validated and inserts it into the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public AdGroupUser(AuthenticatedUserDTO user, System.Int64 adGroupId, System.Int64 userId)
        {
            AdGroupId = adGroupId;
            UserId = userId;
            if (Validate(user, true).Result)
            {
                Insert().Wait();
            }
        }
        #endregion

        #region Attributes
        [AttributeDescriptor("Ad Group User Id", true, EnumValueRanges.Positive)]
        public System.Int64 AdGroupUserId { get; set; }

        [AttributeDescriptor("Ad Group Id", true)]
        public System.Int64 AdGroupId { get; set; }

        [AttributeDescriptor("User Id", true)]
        public System.Int64 UserId { get; set; }

        public AdGroup AdGroup { get => GetManyToOneData<AdGroup>().Result; }
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

                if (!newRecord && AdGroupUserId <= 0)
                {
                    errors.Add(new DomainError("AdGroupUserId", await globalization.GetString(lang, "AdGroupUser001")));
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
