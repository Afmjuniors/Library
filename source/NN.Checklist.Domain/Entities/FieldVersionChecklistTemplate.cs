
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
    public class FieldVersionChecklistTemplate : DomainBase<FieldVersionChecklistTemplate, IFieldVersionChecklistTemplateRepository<FieldVersionChecklistTemplate, System.Int64>, System.Int64>
    {

        #region Constructors

        public FieldVersionChecklistTemplate()
        {

        }
        
        public FieldVersionChecklistTemplate(long? actionUserId, EnumFieldDataType fieldDataTypeId, System.String format, System.Boolean isKey, System.Boolean mandatory, System.Int32 position, System.String regexValidation, System.String title, System.Int64 versionChecklistTemplateId)
        {

            var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

            FieldDataTypeId = fieldDataTypeId; 
            Format = format; 
            IsKey = isKey; 
            Mandatory = mandatory; 
            Position = position; 
            RegexValidation = regexValidation; 
            Title = title; 
            VersionChecklistTemplateId = versionChecklistTemplateId; 


            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Validate(true).Result)
                {
                    Insert().Wait();

                    auditTrail.AddRecord("AT_FieldVersionChecklistTemplateInserted", FieldVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
                }
                tran.Complete();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("field_version_checklist_template_id", true, EnumValueRanges.Positive)]
        public System.Int64 FieldVersionChecklistTemplateId { get; internal set; }

        [AttributeDescriptor("field_data_type_id", true)] 
        public EnumFieldDataType FieldDataTypeId { get; set; }

        [AttributeDescriptor("format", false)] 
        public System.String Format { get; set; }

        [AttributeDescriptor("is_key", true)] 
        public System.Boolean IsKey { get; set; }

        [AttributeDescriptor("mandatory", true)] 
        public System.Boolean Mandatory { get; set; }

        [AttributeDescriptor("position", true)] 
        public System.Int32 Position { get; set; }

        [AttributeDescriptor("regex_validation", false)] 
        public System.String RegexValidation { get; set; }

        [AttributeDescriptor("title", true)] 
        public System.String Title { get; set; }

        [AttributeDescriptor("version_checklist_template_id", true)] 
        public System.Int64 VersionChecklistTemplateId { get; set; }

        public VersionChecklistTemplate VersionChecklistTemplate { get => GetManyToOneData<VersionChecklistTemplate>().Result; }



        #endregion

        #region Validation

        
        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                List<DomainError> erros = new List<DomainError>();

                if (!newRecord && FieldVersionChecklistTemplateId <= 0)
                {
                    erros.Add(new DomainError("field_version_checklist_template_id", "InvalidFieldVersionChecklistTemplateIdIdentifier"));
                }
                else
                {
                                        if (String.IsNullOrEmpty(Title))
                    {
                        erros.Add(new DomainError("title", "TitleInvalid"));
                    }
                    
                    if (Title != null && Title.Length > 100)
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
        
        
        public async Task Update(long? actionUserId, EnumFieldDataType fieldDataTypeId, System.String format, System.Boolean isKey, System.Boolean mandatory, System.Int32 position, System.String regexValidation, System.String title, System.Int64 versionChecklistTemplateId)
        {
            try
            {
                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();
                FieldDataTypeId = fieldDataTypeId; 
                Format = format; 
                IsKey = isKey; 
                Mandatory = mandatory; 
                Position = position; 
                RegexValidation = regexValidation; 
                Title = title; 
                VersionChecklistTemplateId = versionChecklistTemplateId; 


                using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                { 
                    if (await Validate(false))
                    {
                        await Update();

                        auditTrail.AddRecord("AT_FieldVersionChecklistTemplateUpdated", FieldVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
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
                throw new DomainException("FieldVersionChecklistTemplateDataUpdateError", ex);
            }
        }

        #endregion

        #region User Code
                    
        

        #endregion
    }
}