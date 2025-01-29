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
//Generated: 10/2021 12:16:47

#endregion

namespace NN.Checklist.Domain.Entities
{
    public class RelationChecklist : DomainBase<RelationChecklist, IRelationChecklistRepository<RelationChecklist, System.Int64>, System.Int64>
    {

        #region Constructors
        /// <summary>
        /// Name: RelationChecklist
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public RelationChecklist()
        {

        }

        /// <summary>
        /// Name: RelationChecklist
        /// Description: Constructor method that receives as parameter name, prefixNumber and validates if it is true.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public RelationChecklist(AuthenticatedUserDTO user, long checklistId, long dependentChecklistId)
        {
            ChecklistId = checklistId;
            DependentChecklistId = dependentChecklistId;
            CreationTimestamp= DateTime.Now;
            CreationUserId = user.UserId;

            if (Validate(user, true).Result)
            {
                Insert().Wait();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("relation_checklist_id", true, EnumValueRanges.Positive)]
        public System.Int64 RelationChecklistId { get; internal set; }

        [AttributeDescriptor("checklist_id", true)]
        public System.Int64 ChecklistId { get; set; }

        [AttributeDescriptor("dependent_checklist_id", true)]
        public System.Int64 DependentChecklistId { get; set; }

        [AttributeDescriptor("creation_timestamp", true)]
        public System.DateTime CreationTimestamp { get; set; }

        [AttributeDescriptor("creation_user_id", true)]
        public System.Int64 CreationUserId { get; set; }
        public Checklist DependentChecklist { get => GetManyToOneData<Checklist>().Result; }





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