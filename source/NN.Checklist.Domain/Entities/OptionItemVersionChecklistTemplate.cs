
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
    public class OptionItemVersionChecklistTemplate : DomainBase<OptionItemVersionChecklistTemplate, IOptionItemVersionChecklistTemplateRepository<OptionItemVersionChecklistTemplate, System.Int64>, System.Int64>
    {

        #region Constructors

        public OptionItemVersionChecklistTemplate()
        {

        }
        
        public OptionItemVersionChecklistTemplate(long? actionUserId, System.Int64 itemVersionChecklistTemplateId, System.String title, System.Int32 value)
        {

            var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

                        ItemVersionChecklistTemplateId = itemVersionChecklistTemplateId; 
            Title = title; 
            Value = value; 


            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Validate(true).Result)
                {
                    Insert().Wait();

                    auditTrail.AddRecord("AT_OptionItemVersionChecklistTemplateInserted", OptionItemVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
                }
                tran.Complete();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("option_item_version_checklist_template_id", true, EnumValueRanges.Positive)]
        public System.Int64 OptionItemVersionChecklistTemplateId { get; internal set; }

        [AttributeDescriptor("item_version_checklist_template_id", true)] 
        public System.Int64 ItemVersionChecklistTemplateId { get; set; }

        [AttributeDescriptor("title", true)] 
        public System.String Title { get; set; }

        [AttributeDescriptor("value", true)] 
        public System.Int32 Value { get; set; }

        public ItemVersionChecklistTemplate ItemVersionChecklistTemplate { get => GetManyToOneData<ItemVersionChecklistTemplate>().Result; }



        #endregion

        #region Validation

        
        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                List<DomainError> erros = new List<DomainError>();

                if (!newRecord && OptionItemVersionChecklistTemplateId <= 0)
                {
                    erros.Add(new DomainError("option_item_version_checklist_template_id", "InvalidOptionItemVersionChecklistTemplateIdIdentifier"));
                }
                else
                {
                                        if (String.IsNullOrEmpty(Title))
                    {
                        erros.Add(new DomainError("title", "TitleInvalid"));
                    }
                    
                    if (Title != null && Title.Length > 50)
                    {
                        erros.Add(new DomainError("title", "TitleInvalidSize"));
                    }
                    
                    
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
        
        
        public async Task Update(long? actionUserId, System.Int64 itemVersionChecklistTemplateId, System.String title, System.Int32 value)
        {
            try
            {
                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();
                            ItemVersionChecklistTemplateId = itemVersionChecklistTemplateId; 
            Title = title; 
            Value = value; 


                using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                { 
                    if (await Validate(false))
                    {
                        await Update();

                        auditTrail.AddRecord("AT_OptionItemVersionChecklistTemplateUpdated", OptionItemVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
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
                throw new DomainException("OptionItemVersionChecklistTemplateDataUpdateError", ex);
            }
        }

        #endregion

        #region User Code
                    
        

        #endregion
    }
}