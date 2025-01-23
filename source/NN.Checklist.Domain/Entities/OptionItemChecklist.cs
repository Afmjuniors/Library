
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
//Generated: 17/12/2024 10:10:11

#endregion

namespace NN.Checklist.Domain.Entities
{
    public class OptionItemChecklist : DomainBase<OptionItemChecklist, IOptionItemChecklistRepository<OptionItemChecklist, System.Int64>, System.Int64>
    {

        #region Constructors

        public OptionItemChecklist()
        {

        }
        
        public OptionItemChecklist(long? actionUserId, System.DateTime creationTimestamp, System.Int64 creationUserId, System.Int64 itemChecklistId, System.Int64 optionItemVersionChecklistTemplateId)
        {

            var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

            CreationTimestamp = creationTimestamp; 
            CreationUserId = creationUserId; 
            ItemChecklistId = itemChecklistId; 
            OptionItemVersionChecklistTemplateId = optionItemVersionChecklistTemplateId;


            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Validate(true).Result)
                {
                    Insert().Wait();

                    auditTrail.AddRecord("AT_OptionItemChecklistInserted", OptionItemChecklistId, EnumSystemFunctionality.Checklists, actionUserId);
                }
                tran.Complete();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("option_item_checklist_id", true, EnumValueRanges.Positive)]
        public System.Int64 OptionItemChecklistId { get; internal set; }

        [AttributeDescriptor("creation_timestamp", true)] 
        public System.DateTime CreationTimestamp { get; set; }

        [AttributeDescriptor("creation_user_id", true)] 
        public System.Int64 CreationUserId { get; set; }

        [AttributeDescriptor("item_checklist_id", true)] 
        public System.Int64 ItemChecklistId { get; set; }

        [AttributeDescriptor("option_item_version_checklist_template_id", true)] 
        public System.Int64 OptionItemVersionChecklistTemplateId { get; set; }
  

        public User CreationUser { get => GetManyToOneData<User>().Result; }

        public OptionItemVersionChecklistTemplate OptionItemVersionChecklistTemplate { get => GetManyToOneData<OptionItemVersionChecklistTemplate>().Result; }



        #endregion

        #region Validation

        
        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                List<DomainError> erros = new List<DomainError>();

                if (!newRecord && OptionItemChecklistId <= 0)
                {
                    erros.Add(new DomainError("option_item_checklist_id", "InvalidOptionItemChecklistIdIdentifier"));
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
        
        
        public async Task Update(long? actionUserId, System.DateTime creationTimestamp, System.Int64 creationUserId, System.Int64 itemChecklistId, System.Int64 optionItemVersionChecklistTemplateId)
        {
            try
            {
                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();
                            CreationTimestamp = creationTimestamp; 
            CreationUserId = creationUserId; 
            ItemChecklistId = itemChecklistId; 
            OptionItemVersionChecklistTemplateId = optionItemVersionChecklistTemplateId; 


                using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                { 
                    if (await Validate(false))
                    {
                        await Update();

                        auditTrail.AddRecord("AT_OptionItemChecklistUpdated", OptionItemChecklistId, EnumSystemFunctionality.Checklists, actionUserId);
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
                throw new DomainException("OptionItemChecklistDataUpdateError", ex);
            }
        }

        #endregion

        #region User Code
                    
        

        #endregion
    }
}