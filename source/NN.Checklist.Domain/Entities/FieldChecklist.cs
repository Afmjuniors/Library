
using TDCore.Core;
using TDCore.Data;
using TDCore.Domain;
using TDCore.Domain.Exceptions;
using TDCore.DependencyInjection;
using NN.Checklist.Domain.Services.Specifications;
using NN.Checklist.Domain.Repositories.Specifications;
using NN.Checklist.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Threading.Tasks;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:10

#endregion

namespace NN.Checklist.Domain.Entities
{
    public class FieldChecklist : DomainBase<FieldChecklist, IFieldChecklistRepository<FieldChecklist, System.Int64>, System.Int64>
    {

        #region Constructors

        public FieldChecklist()
        {

        }
        
        public FieldChecklist(long? actionUserId, System.Int64 checklistId, System.DateTime creationTimestamp, System.Int64 creationUserId, System.Int64 fieldVersionChecklistTemplateId, System.Int64? optionFieldVersionChecklistTemplateId, System.DateTime? updateTimestamp, System.Int64? updateUserId, System.String value)
        {

            var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

                        ChecklistId = checklistId; 
            CreationTimestamp = creationTimestamp; 
            CreationUserId = creationUserId; 
            FieldVersionChecklistTemplateId = fieldVersionChecklistTemplateId; 
            OptionFieldVersionChecklistTemplateId = optionFieldVersionChecklistTemplateId; 
            UpdateTimestamp = updateTimestamp; 
            UpdateUserId = updateUserId; 
            Value = value; 


            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Validate(true).Result)
                {
                    Insert().Wait();

                    auditTrail.AddRecord("AT_FieldChecklistInserted", FieldChecklistId, EnumSystemFunctionality.Checklists, actionUserId);
                }
                tran.Complete();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("field_checklist_id", true, EnumValueRanges.Positive)]
        public System.Int64 FieldChecklistId { get; internal set; }

        [AttributeDescriptor("checklist_id", true)] 
        public System.Int64 ChecklistId { get; set; }

        [AttributeDescriptor("creation_timestamp", true)] 
        public System.DateTime CreationTimestamp { get; set; }

        [AttributeDescriptor("creation_user_id", true)] 
        public System.Int64 CreationUserId { get; set; }

        [AttributeDescriptor("field_version_checklist_template_id", true)] 
        public System.Int64 FieldVersionChecklistTemplateId { get; set; }

        [AttributeDescriptor("option_field_version_checklist_template_id", false)] 
        public System.Int64? OptionFieldVersionChecklistTemplateId { get; set; }

        [AttributeDescriptor("update_timestamp", false)] 
        public System.DateTime? UpdateTimestamp { get; set; }

        [AttributeDescriptor("update_user_id", false)] 
        public System.Int64? UpdateUserId { get; set; }

        [AttributeDescriptor("value", false)] 
        public System.String Value { get; set; }

        public Checklist Checklist { get => GetManyToOneData<Checklist>().Result; }

        public User CreationUser { get => GetManyToOneData<User>().Result; }

        public FieldVersionChecklistTemplate FieldVersionChecklistTemplate { get => GetManyToOneData<FieldVersionChecklistTemplate>().Result; }

        public OptionFieldVersionChecklistTemplate OptionFieldVersionChecklistTemplate { get => GetManyToOneData<OptionFieldVersionChecklistTemplate>().Result; }

        public User UpdateUser { get => GetManyToOneData<User>().Result; }



        #endregion

        #region Validation

        
        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                List<DomainError> erros = new List<DomainError>();

                if (!newRecord && FieldChecklistId <= 0)
                {
                    erros.Add(new DomainError("field_checklist_id", "InvalidFieldChecklistIdIdentifier"));
                }
                else
                {
                                        
                }

                if (erros.Count > 0)
                {
                    throw new DomainException("DataConsistencyError", erros);
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

        #region Save
        
        
        public async Task Update(long? actionUserId, System.Int64 checklistId, System.DateTime creationTimestamp, System.Int64 creationUserId, System.Int64 fieldVersionChecklistTemplateId, System.Int64? optionFieldVersionChecklistTemplateId, System.DateTime? updateTimestamp, System.Int64? updateUserId, System.String value)
        {
            try
            {
                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();
                            ChecklistId = checklistId; 
            CreationTimestamp = creationTimestamp; 
            CreationUserId = creationUserId; 
            FieldVersionChecklistTemplateId = fieldVersionChecklistTemplateId; 
            OptionFieldVersionChecklistTemplateId = optionFieldVersionChecklistTemplateId; 
            UpdateTimestamp = updateTimestamp; 
            UpdateUserId = updateUserId; 
            Value = value; 


                using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                { 
                    if (await Validate(false))
                    {
                        await Update();

                        auditTrail.AddRecord("AT_FieldChecklistUpdated", FieldChecklistId, EnumSystemFunctionality.Checklists, actionUserId);
                    }
                    tran.Complete();
                }
            }
            catch (DomainException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DomainException("FieldChecklistDataUpdateError", ex);
            }
        }

        #endregion

        #region User Code
                    
        

        #endregion
    }
}