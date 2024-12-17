
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
//Generated: 17/12/2024 10:10:12

#endregion

namespace NN.Checklist.Domain.Entities
{
    public class CancelledItemVersionChecklistTemplate : DomainBase<CancelledItemVersionChecklistTemplate, ICancelledItemVersionChecklistTemplateRepository<CancelledItemVersionChecklistTemplate, System.Int64>, System.Int64>
    {

        #region Constructors

        public CancelledItemVersionChecklistTemplate()
        {

        }
        
        public CancelledItemVersionChecklistTemplate(long? actionUserId, System.Int64 itemVersionChecklistTemplateId, System.Int64 targetItemVersionChecklistTemplateId)
        {

            var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

                        ItemVersionChecklistTemplateId = itemVersionChecklistTemplateId; 
            TargetItemVersionChecklistTemplateId = targetItemVersionChecklistTemplateId; 


            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Validate(true).Result)
                {
                    Insert().Wait();

                    auditTrail.AddRecord("AT_CancelledItemVersionChecklistTemplateInserted", CancelledItemVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
                }
                tran.Complete();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("cancelled_item_version_checklist_template_id", true, EnumValueRanges.Positive)]
        public System.Int64 CancelledItemVersionChecklistTemplateId { get; set; }

        [AttributeDescriptor("item_version_checklist_template_id", true)] 
        public System.Int64 ItemVersionChecklistTemplateId { get; set; }

        [AttributeDescriptor("target_item_version_checklist_template_id", true)] 
        public System.Int64 TargetItemVersionChecklistTemplateId { get; set; }

        public ItemVersionChecklistTemplate ItemVersionChecklistTemplate { get => GetManyToOneData<ItemVersionChecklistTemplate>().Result; }

        public ItemVersionChecklistTemplate TargetItemVersionChecklistTemplate { get => GetManyToOneData<ItemVersionChecklistTemplate>().Result; }



        #endregion

        #region Validation

        
        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                List<DomainError> erros = new List<DomainError>();

                if (!newRecord && CancelledItemVersionChecklistTemplateId <= 0)
                {
                    erros.Add(new DomainError("cancelled_item_version_checklist_template_id", "InvalidCancelledItemVersionChecklistTemplateIdIdentifier"));
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
        
        
        public async Task Update(long? actionUserId, System.Int64 itemVersionChecklistTemplateId, System.Int64 targetItemVersionChecklistTemplateId)
        {
            try
            {
                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();
                            ItemVersionChecklistTemplateId = itemVersionChecklistTemplateId; 
            TargetItemVersionChecklistTemplateId = targetItemVersionChecklistTemplateId; 


                using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                { 
                    if (await Validate(false))
                    {
                        await Update();

                        auditTrail.AddRecord("AT_CancelledItemVersionChecklistTemplateUpdated", CancelledItemVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
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
                throw new DomainException("CancelledItemVersionChecklistTemplateDataUpdateError", ex);
            }
        }

        #endregion

        #region User Code
                    
        

        #endregion
    }
}