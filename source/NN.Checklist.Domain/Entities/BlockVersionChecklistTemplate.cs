
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
    public class BlockVersionChecklistTemplate : DomainBase<BlockVersionChecklistTemplate, IBlockVersionChecklistTemplateRepository<BlockVersionChecklistTemplate, System.Int64>, System.Int64>
    {

        #region Constructors

        public BlockVersionChecklistTemplate()
        {

        }
        
        public BlockVersionChecklistTemplate(long? actionUserId, System.Int64? parentBlockVersionChecklistTemplateId, System.Int32 position, System.String title, System.Int64 versionChecklistTemplateId)
        {

            var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

                        ParentBlockVersionChecklistTemplateId = parentBlockVersionChecklistTemplateId; 
            Position = position; 
            Title = title; 
            VersionChecklistTemplateId = versionChecklistTemplateId; 


            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Validate(true).Result)
                {
                    Insert().Wait();

                    auditTrail.AddRecord("AT_BlockVersionChecklistTemplateInserted", BlockVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
                }
                tran.Complete();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("block_version_checklist_template_id", true, EnumValueRanges.Positive)]
        public System.Int64 BlockVersionChecklistTemplateId { get; internal set; }

        [AttributeDescriptor("parent_block_version_checklist_template_id", false)] 
        public System.Int64? ParentBlockVersionChecklistTemplateId { get; set; }

        [AttributeDescriptor("position", true)] 
        public System.Int32 Position { get; set; }

        [AttributeDescriptor("title", true)] 
        public System.String Title { get; set; }

        [AttributeDescriptor("version_checklist_template_id", true)] 
        public System.Int64 VersionChecklistTemplateId { get; set; }

        public BlockVersionChecklistTemplate ParentBlockVersionChecklistTemplate { get => GetManyToOneData<BlockVersionChecklistTemplate>().Result; }

        public VersionChecklistTemplate VersionChecklistTemplate { get => GetManyToOneData<VersionChecklistTemplate>().Result; }



        #endregion

        #region Validation

        
        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                List<DomainError> erros = new List<DomainError>();

                if (!newRecord && BlockVersionChecklistTemplateId <= 0)
                {
                    erros.Add(new DomainError("block_version_checklist_template_id", "InvalidBlockVersionChecklistTemplateIdIdentifier"));
                }
                else
                {
                                        if (String.IsNullOrEmpty(Title))
                    {
                        erros.Add(new DomainError("title", "TitleInvalid"));
                    }
                    
                    if (Title != null && Title.Length > 150)
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
        
        
        public async Task Update(long? actionUserId, System.Int64? parentBlockVersionChecklistTemplateId, System.Int32 position, System.String title, System.Int64 versionChecklistTemplateId)
        {
            try
            {
                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();
                            ParentBlockVersionChecklistTemplateId = parentBlockVersionChecklistTemplateId; 
            Position = position; 
            Title = title; 
            VersionChecklistTemplateId = versionChecklistTemplateId; 


                using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                { 
                    if (await Validate(false))
                    {
                        await Update();

                        auditTrail.AddRecord("AT_BlockVersionChecklistTemplateUpdated", BlockVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
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
                throw new DomainException("BlockVersionChecklistTemplateDataUpdateError", ex);
            }
        }

        #endregion

        #region User Code
                    
        

        #endregion
    }
}