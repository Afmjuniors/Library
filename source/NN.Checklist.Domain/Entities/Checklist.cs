
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
    public class Checklist : DomainBase<Checklist, IChecklistRepository<Checklist, System.Int64>, System.Int64>
    {

        #region Constructors

        public Checklist()
        {

        }
        
        public Checklist(long? actionUserId, System.DateTime creationTimestamp, System.Int64 creationUserId, System.DateTime? updateTimestamp, System.Int64? updateUserId, System.Int64 versionChecklistTemplateId)
        {

            var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

                        CreationTimestamp = creationTimestamp; 
            CreationUserId = creationUserId; 
            UpdateTimestamp = updateTimestamp; 
            UpdateUserId = updateUserId; 
            VersionChecklistTemplateId = versionChecklistTemplateId; 


            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Validate(true).Result)
                {
                    Insert().Wait();

                    auditTrail.AddRecord("AT_ChecklistInserted", ChecklistId, EnumSystemFunctionality.Checklists, actionUserId);
                }
                tran.Complete();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("checklist_id", true, EnumValueRanges.Positive)]
        public System.Int64 ChecklistId { get; internal set; }

        [AttributeDescriptor("creation_timestamp", true)] 
        public System.DateTime CreationTimestamp { get; set; }

        [AttributeDescriptor("creation_user_id", true)] 
        public System.Int64 CreationUserId { get; set; }

        [AttributeDescriptor("update_timestamp", false)] 
        public System.DateTime? UpdateTimestamp { get; set; }

        [AttributeDescriptor("update_user_id", false)] 
        public System.Int64? UpdateUserId { get; set; }

        [AttributeDescriptor("version_checklist_template_id", true)] 
        public System.Int64 VersionChecklistTemplateId { get; set; }

        public User CreationUser { get => GetManyToOneData<User>().Result; }

        public User UpdateUser { get => GetManyToOneData<User>().Result; }

        public VersionChecklistTemplate VersionChecklistTemplate { get => GetManyToOneData<VersionChecklistTemplate>().Result; }



        #endregion

        #region Validation

        
        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                List<DomainError> erros = new List<DomainError>();

                if (!newRecord && ChecklistId <= 0)
                {
                    erros.Add(new DomainError("checklist_id", "InvalidChecklistIdIdentifier"));
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
        
        
        public async Task Update(long? actionUserId, System.DateTime creationTimestamp, System.Int64 creationUserId, System.DateTime? updateTimestamp, System.Int64? updateUserId, System.Int64 versionChecklistTemplateId)
        {
            try
            {
                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();
                            CreationTimestamp = creationTimestamp; 
            CreationUserId = creationUserId; 
            UpdateTimestamp = updateTimestamp; 
            UpdateUserId = updateUserId; 
            VersionChecklistTemplateId = versionChecklistTemplateId; 


                using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                { 
                    if (await Validate(false))
                    {
                        await Update();

                        auditTrail.AddRecord("AT_ChecklistUpdated", ChecklistId, EnumSystemFunctionality.Checklists, actionUserId);
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
                throw new DomainException("ChecklistDataUpdateError", ex);
            }
        }

        #endregion

        #region User Code
                    
        

        #endregion
    }
}