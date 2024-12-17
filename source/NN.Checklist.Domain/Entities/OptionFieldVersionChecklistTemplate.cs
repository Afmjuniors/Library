
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
    public class OptionFieldVersionChecklistTemplate : DomainBase<OptionFieldVersionChecklistTemplate, IOptionFieldVersionChecklistTemplateRepository<OptionFieldVersionChecklistTemplate, System.Int64>, System.Int64>
    {

        #region Constructors

        public OptionFieldVersionChecklistTemplate()
        {

        }
        
        public OptionFieldVersionChecklistTemplate(long? actionUserId, System.Int64 fieldVersionChecklistTemplateId, System.String title, System.Int32 value)
        {

            var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

                        FieldVersionChecklistTemplateId = fieldVersionChecklistTemplateId; 
            Title = title; 
            Value = value; 


            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Validate(true).Result)
                {
                    Insert().Wait();

                    auditTrail.AddRecord("AT_OptionFieldVersionChecklistTemplateInserted", OptionFieldVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
                }
                tran.Complete();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("option_field_version_checklist_template_id", true, EnumValueRanges.Positive)]
        public System.Int64 OptionFieldVersionChecklistTemplateId { get; internal set; }

        [AttributeDescriptor("field_version_checklist_template_id", true)] 
        public System.Int64 FieldVersionChecklistTemplateId { get; set; }

        [AttributeDescriptor("title", true)] 
        public System.String Title { get; set; }

        [AttributeDescriptor("value", true)] 
        public System.Int32 Value { get; set; }

        public FieldVersionChecklistTemplate FieldVersionChecklistTemplate { get => GetManyToOneData<FieldVersionChecklistTemplate>().Result; }



        #endregion

        #region Validation

        
        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                List<DomainError> erros = new List<DomainError>();

                if (!newRecord && OptionFieldVersionChecklistTemplateId <= 0)
                {
                    erros.Add(new DomainError("option_field_version_checklist_template_id", "InvalidOptionFieldVersionChecklistTemplateIdIdentifier"));
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
        
        
        public async Task Update(long? actionUserId, System.Int64 fieldVersionChecklistTemplateId, System.String title, System.Int32 value)
        {
            try
            {
                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();
                            FieldVersionChecklistTemplateId = fieldVersionChecklistTemplateId; 
            Title = title; 
            Value = value; 


                using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                { 
                    if (await Validate(false))
                    {
                        await Update();

                        auditTrail.AddRecord("AT_OptionFieldVersionChecklistTemplateUpdated", OptionFieldVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
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
                throw new DomainException("OptionFieldVersionChecklistTemplateDataUpdateError", ex);
            }
        }

        #endregion

        #region User Code
                    
        

        #endregion
    }
}