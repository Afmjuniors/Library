using NN.Checklist.Domain.Common;
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
//Generated: 10/2021 16:03:49

#endregion

namespace NN.Checklist.Domain.Entities
{
    public class SystemRecord : DomainBaseSRM<SystemRecord, ISystemRecordRepository<SystemRecord, System.Int64>, System.Int64>
    {

        #region Constructors

        /// <summary>
        /// Name: SystemRecord
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public SystemRecord()
        {

        }

        /// <summary>
        /// Name: SystemRecord
        /// Description: Constructor method that receives as parameter description, id, systemFunctionality, userId and receives the information and performs a validation, if true, inserts it into the database, otherwise an exception is thrown.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        public SystemRecord(System.String description, System.Int64? id, EnumSystemFunctionality systemFunctionality, System.Int64? userId)
        {
            try
            {
                DateTime = DateTime.Now;
                Description = description;
                Id = id;
                SystemFunctionalityId = systemFunctionality;
                UserId = userId;
                Comments = null;

                if (Validate(null, true).Result)
                {
                    Insert().Wait();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(TDCore.Core.Logging.LogType.Error, ex);
            }            
        }

        /// <summary>
        /// Name: SystemRecord
        /// Description: Constructor method that receives as parameter description, id, systemFunctionality, userId, comments and receives the information and performs a validation, if true, inserts it into the database, otherwise an exception is thrown.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        public SystemRecord(System.String description, System.Int64? id, EnumSystemFunctionality systemFunctionality, System.Int64? userId, string comments)
        {
            try
            {
                DateTime = DateTime.Now;
                Description = description;
                Id = id;
                SystemFunctionalityId = systemFunctionality;
                UserId = userId;
                Comments = comments;

                if (Validate(null, true).Result)
                {
                    Insert().Wait();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(TDCore.Core.Logging.LogType.Error, ex);
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("System Record Id", true, EnumValueRanges.Positive)]
        public System.Int64 SystemRecordId { get; internal set; }

        [AttributeDescriptor("Date Time", true)] 
        public System.DateTime DateTime { get; set; }

        [AttributeDescriptor("Description", true)] 
        public System.String Description { get; set; }

        [AttributeDescriptor("Id", true)] 
        public System.Int64? Id { get; set; }

        [AttributeDescriptor("System Functionality Id", true)] 
        public EnumSystemFunctionality SystemFunctionalityId { get; set; }

        [AttributeDescriptor("User Id", true)] 
        public System.Int64? UserId { get; set; }

        [AttributeDescriptor("Comments", false)]
        public string Comments { get; set; }

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

                if (!newRecord && SystemRecordId <= 0)
                {
                    errors.Add(new DomainError("SystemRecordId", await globalization.GetString(lang, "SystemRecord001")));
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