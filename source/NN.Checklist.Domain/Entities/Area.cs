using NN.Checklist.Domain.Common;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Entities.Bases;
using NN.Checklist.Domain.Repositories.Specifications;
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
//Generated: 10/2021 12:16:42

#endregion

namespace NN.Checklist.Domain.Entities
{
    public class Area : DomainBaseSRM<Area, IAreaRepository<Area, System.Int64>, System.Int64>
    {

        #region Constructors
        /// <summary>
        /// Name: Area
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public Area()
        {

        }

        /// <summary>
        /// Name: Area
        /// Description: Constructor method that receives as parameter user, name, description, processId, phones, comments and if the validation is true it inserts the phone number and if the user is not null it displays the number and shows a message.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public Area(AuthenticatedUserDTO user, string name, System.String description, System.Int64 processId, string comments)
        {
            var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

            string lang = null;
            string and = "";

            if (user != null)
            {
                lang = user.CultureInfo;
            }
            
            Name = name;
            Description = description; 
            ProcessId = processId; 
            if (Validate(user, true).Result)
            {
                using (var tran = new TransactionScope())
                {
                    Insert().Wait();
                    tran.Complete();
                }
            }

            if (user != null)
            {
                var process = Process.Repository.Get(processId);

                var parameters = $"Name: {Name}, Description: {Description}, Process: {Process.Description}";

                var msg = globalization.GetString(globalization.DefaultLanguage, "NewArea",
                            new string[] { parameters, "" }).Result;
                new SystemRecord(msg, AreaId, EnumSystemFunctionality.GeneralRegistrations, user != null ? user.UserId : null, comments);
            }
        }
        #endregion

        #region Attributes

        [AttributeDescriptor("Id", true, EnumValueRanges.Positive)]
        public System.Int64 AreaId { get; internal set; }

        [AttributeDescriptor("Name", true)]
        public System.String Name { get; set; }

        [AttributeDescriptor("Description", true)] 
        public System.String Description { get; set; }

        [AttributeDescriptor("Process Id", true)] 
        public System.Int64 ProcessId { get; set; }

        public Process Process { get => GetManyToOneData<Process>().Result; }

        #endregion

        #region User Code


        /// <summary>
        /// Name: Update
        /// Description: Method that receives as parameter user, name, description, processId, comments and if the validation is true it creates a copy of the area, if it creates a new instance and updates and displays a message.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task Update(AuthenticatedUserDTO user, string name, string description, long processId, string comments)
        {
            try
            {
                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

                string lang = null;

                var before = (Area)this.MemberwiseClone();

                Name = name;
                Description = description;
                ProcessId = processId;

                if (await Validate(user, false))
                {
                    using (var trans = new TransactionScope())
                    {
                        await Update();
                        trans.Complete();
                    }

                    if (user != null)
                    {
                        var msg = await globalization.GetString(globalization.DefaultLanguage, "UpdatedArea", new string[] { GetDifferences(this, before) });
                        new SystemRecord(msg, AreaId, EnumSystemFunctionality.GeneralRegistrations, user != null ? user.UserId : null, comments);
                    }
                }
            }
            catch (DomainException ex)
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

                if (!newRecord && AreaId <= 0)
                {
                    errors.Add(new DomainError("AreaId", await globalization.GetString(lang, "Area001")));
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