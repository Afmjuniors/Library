
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
    public class VersionChecklistTemplate : DomainBase<VersionChecklistTemplate, IVersionChecklistTemplateRepository<VersionChecklistTemplate, System.Int64>, System.Int64>
    {

        #region Constructors

        public VersionChecklistTemplate()
        {

        }
        
        public VersionChecklistTemplate(long? actionUserId, System.Int64 checklistTemplateId, System.Int64 creationUserId, System.DateTime timestampCreation, System.DateTime? timestampUpdate, System.Int64? updateUserId, System.String version)
        {

            var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

                        ChecklistTemplateId = checklistTemplateId; 
            CreationUserId = creationUserId; 
            TimestampCreation = timestampCreation; 
            TimestampUpdate = timestampUpdate; 
            UpdateUserId = updateUserId; 
            Version = version; 


            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Validate(true).Result)
                {
                    Insert().Wait();

                    auditTrail.AddRecord("AT_VersionChecklistTemplateInserted", VersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
                }
                tran.Complete();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("version_checklist_template_id", true, EnumValueRanges.Positive)]
        public System.Int64 VersionChecklistTemplateId { get; internal set; }

        [AttributeDescriptor("checklist_template_id", true)] 
        public System.Int64 ChecklistTemplateId { get; set; }

        [AttributeDescriptor("creation_user_id", true)] 
        public System.Int64 CreationUserId { get; set; }

        [AttributeDescriptor("timestamp_creation", true)] 
        public System.DateTime TimestampCreation { get; set; }

        [AttributeDescriptor("timestamp_update", false)] 
        public System.DateTime? TimestampUpdate { get; set; }

        [AttributeDescriptor("update_user_id", false)] 
        public System.Int64? UpdateUserId { get; set; }

        [AttributeDescriptor("version", true)] 
        public System.String Version { get; set; }

        public ChecklistTemplate ChecklistTemplate { get => GetManyToOneData<ChecklistTemplate>().Result; }
        

        public IList<BlockVersionChecklistTemplate> BlocksChecklistTemplate { get => GetOneToManyData<BlockVersionChecklistTemplate>().Result; set { } }


        public IList<FieldVersionChecklistTemplate> FieldsVersionChecklistsTemplate { get => GetOneToManyData<FieldVersionChecklistTemplate>().Result; set { } }


        public User CreationUser { get => GetManyToOneData<User>().Result; }



        #endregion

        #region Validation

        
        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                List<DomainError> erros = new List<DomainError>();

                if (!newRecord && VersionChecklistTemplateId <= 0)
                {
                    erros.Add(new DomainError("version_checklist_template_id", "InvalidVersionChecklistTemplateIdIdentifier"));
                }
                else
                {
                                        if (String.IsNullOrEmpty(Version))
                    {
                        erros.Add(new DomainError("version", "VersionInvalid"));
                    }
                    
                    if (Version != null && Version.Length > 10)
                    {
                        erros.Add(new DomainError("version", "VersionInvalidSize"));
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
        
        
        public async Task Update(long? actionUserId, System.Int64 checklistTemplateId, System.Int64 creationUserId, System.DateTime timestampCreation, System.DateTime? timestampUpdate, System.Int64? updateUserId, System.String version)
        {
            try
            {
                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();
                            ChecklistTemplateId = checklistTemplateId; 
            CreationUserId = creationUserId; 
            TimestampCreation = timestampCreation; 
            TimestampUpdate = timestampUpdate; 
            UpdateUserId = updateUserId; 
            Version = version; 


                using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                { 
                    if (await Validate(false))
                    {
                        await Update();

                        auditTrail.AddRecord("AT_VersionChecklistTemplateUpdated", VersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
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
                throw new DomainException("VersionChecklistTemplateDataUpdateError", ex);
            }
        }

        #endregion

        #region User Code
                    
        

        #endregion
    }
}