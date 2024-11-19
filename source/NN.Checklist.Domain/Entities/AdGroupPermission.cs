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
//Generated: 10/2021 12:16:45

#endregion

namespace NN.Checklist.Domain.Entities
{
    public class AdGroupPermission : DomainBaseSRM<AdGroupPermission, IAdGroupPermissionRepository<AdGroupPermission, System.Int64>, System.Int64>
    {

        #region Constructors
        /// <summary>
        /// Name: AdGroupPermission
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public AdGroupPermission()
        {

        }

        /// <summary>
        /// Name: AdGroupPermission
        /// Description: Constructor method that receives as a parameter adGroupId, permissionId and checks if the data has been validated and inserts it into the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public AdGroupPermission(AuthenticatedUserDTO user, System.Int64 adGroupId, System.Int64 permissionId)
        {
            AdGroupId = adGroupId; 
            PermissionId = permissionId;
            if (Validate(user, true).Result)
            {
                Insert().Wait();
            }
        }
        #endregion

        #region Attributes
        [AttributeDescriptor("Ad Group Permission Id", true, EnumValueRanges.Positive)]
        public System.Int64 AdGroupPermissionId { get; set; }

        [AttributeDescriptor("Ad Group Id", true)] 
        public System.Int64 AdGroupId { get; set; }

        [AttributeDescriptor("Permission Id", true)] 
        public System.Int64 PermissionId { get; set; }

        public AdGroup AdGroup { get => GetManyToOneData<AdGroup>().Result; }
        public Permission Permission { get => GetManyToOneData<Permission>().Result; }
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

                if (!newRecord && AdGroupPermissionId <= 0)
                {
                    errors.Add(new DomainError("AdGroupPermissionId", await globalization.GetString(lang, "AdGroupPermission001")));
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