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
//Generated: 10/2021 12:16:43

#endregion

namespace NN.Checklist.Domain.Entities
{
    public class Process : DomainBaseSRM<Process, IProcessRepository<Process, System.Int64>, System.Int64>
    {

        #region Constructors

        /// <summary>
        /// Name: Process
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public Process()
        {

        }

        /// <summary>
        /// Name: Process
        /// Description: Constructor method that receives as parameter acronym, description, connectionStringAlarms, queryAlarms, connectionStringEvents, queryEvents, connectionStringExtreme, queryExtreme, and performs a validation, if true, is inserted in the database.
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        
        public Process(AuthenticatedUserDTO user, System.String acronym, System.String description, string connectionStringAlarms, string queryAlarms, string connectionStringEvents, string queryEvents, string connectionStringExtreme, string queryExtreme)
        {
            Acronym = acronym; 
            Description = description;
            ConnectionStringAlarms = connectionStringAlarms;
            QueryAlarms = queryAlarms;
            ConnectionStringEvents = connectionStringEvents;
            QueryEvents = queryEvents;
            ConnectionStringExtreme = connectionStringExtreme;
            QueryExtreme = queryExtreme;

            if (Validate(user, true).Result)
            {
                Insert().Wait();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("Process Id", true, EnumValueRanges.Positive)]
        public System.Int64 ProcessId { get; set; }

        [AttributeDescriptor("Acronym", true)] 
        public System.String Acronym { get; set; }

        [AttributeDescriptor("Description", true)] 
        public System.String Description { get; set; }

        [AttributeDescriptor("Connection String Alarms", true)]
        public System.String ConnectionStringAlarms { get; set; }

        [AttributeDescriptor("Query Alarms", true)]
        public System.String QueryAlarms { get; set; }

        [AttributeDescriptor("Connection String Events", true)]
        public System.String ConnectionStringEvents { get; set; }

        [AttributeDescriptor("Query Events", true)]
        public System.String QueryEvents { get; set; }

        [AttributeDescriptor("Connection String Extreme", false)]
        public System.String ConnectionStringExtreme { get; set; }

        [AttributeDescriptor("Query Extreme", false)]
        public System.String QueryExtreme { get; set; }

        [AttributeDescriptor("First Id", false)]
        public long? FirstId { get; set; }

        [AttributeDescriptor("Last Timestamp", false)]
        public DateTime? LastTimestamp { get; set; }

        #endregion

        #region User Code

        /// <summary>
        /// Name: UpdateFirstId
        /// Description: Method that updates the first id in the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>

        public async Task UpdateFirstId(long id)
        {
            try
            {
                FirstId = id;

                await Update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Name: UpdateLastTimestamp
        /// Description: Method that updates last timestamp in the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task UpdateLastTimestamp(DateTime timestamp)
        {
            try
            {
                LastTimestamp = timestamp;

                await Update();
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

                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
                string lang = null;

                if (user != null)
                {
                    lang = user.CultureInfo;
                }

                List<DomainError> errors = new List<DomainError>();

                if (!newRecord && ProcessId <= 0)
                {
                    errors.Add(new DomainError("ProcessId", await globalization.GetString(lang, "Process001")));
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