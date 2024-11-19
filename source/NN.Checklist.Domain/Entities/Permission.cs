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
    public class Permission : DomainBaseSRM<Permission, IPermissionRepository<Permission, System.Int64>, System.Int64>
    {

        #region Constructors

        /// <summary>
        /// Name: Permission
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public Permission()
        {

        }

        /// <summary>
        /// Name: Permission
        /// Description: Constructor method that receives as a parameter description, name and, if the validation is true, inserts it into the database.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        
        public Permission(AuthenticatedUserDTO user, System.String description, System.String name)
        {
            Description = description;
            Name = name;

            if (Validate(user, true).Result)
            {
                Insert().Wait();
            }
        }

        /// <summary>
        /// Name: Permission
        /// Description: Constructor method that receives as a parameter id, description, name and, if the validation is true, inserts it into the database.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public Permission(long id, System.String description, System.String name)
        {
            PermissionId = id;
            Description = description; 
            Name = name; 

            if (Validate(true))
            {
                Insert();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("Permission Id", true, EnumValueRanges.Positive)]
        public System.Int64 PermissionId { get; set; }

        [AttributeDescriptor("Description", true)] 
        public System.String Description { get; set; }

        [AttributeDescriptor("Name", true)] 
        public System.String Name { get; set; }



        #endregion

        #region User Code

        /// <summary>
        /// Name: Update
        /// Description: Method that receives as a parameter description, name and, if the validation is false, updates it in the database.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public async Task Update(AuthenticatedUserDTO user, System.String description, System.String name)
        {
            try
            {
                Description = description;
                Name = name;

                if (await Validate(user, false))
                {
                    await Update();
                }
            }
            catch (Exception)
            {

                throw;
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

                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
                string lang = null;

                if (user != null)
                {
                    lang = user.CultureInfo;
                }

                List<DomainError> errors = new List<DomainError>();

                if (!newRecord && PermissionId <= 0)
                {
                    errors.Add(new DomainError("PermissionId", await globalization.GetString(lang, "Permission001")));
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