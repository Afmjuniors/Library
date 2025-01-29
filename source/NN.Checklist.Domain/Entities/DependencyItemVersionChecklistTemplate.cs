
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
    public class DependencyItemVersionChecklistTemplate : DomainBase<DependencyItemVersionChecklistTemplate, IDependencyItemVersionChecklistTemplateRepository<DependencyItemVersionChecklistTemplate, System.Int64>, System.Int64>
    {

        #region Constructors

        public DependencyItemVersionChecklistTemplate()
        {

        }

        public DependencyItemVersionChecklistTemplate(long? actionUserId, System.Int64? dependentBlockVersionChecklistTemplateId, System.Int64? dependentItemVersionChecklistTemplateId, System.Int64 itemVersionChecklistTemplateId)
        {

            var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

            DependentBlockVersionChecklistTemplateId = dependentBlockVersionChecklistTemplateId;
            DependentItemVersionChecklistTemplateId = dependentItemVersionChecklistTemplateId;
            ItemVersionChecklistTemplateId = itemVersionChecklistTemplateId;


            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Validate(true).Result)
                {
                    Insert().Wait();

                    auditTrail.AddRecord("AT_DependencyItemVersionChecklistTemplateInserted", DependencyItemVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
                }
                tran.Complete();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("dependency_item_version_checklist_template_id", true, EnumValueRanges.Positive)]
        public System.Int64 DependencyItemVersionChecklistTemplateId { get; internal set; }

        [AttributeDescriptor("dependent_block_version_checklist_template_id", false)]
        public System.Int64? DependentBlockVersionChecklistTemplateId { get; set; }

        [AttributeDescriptor("dependent_item_version_checklist_template_id", false)]
        public System.Int64? DependentItemVersionChecklistTemplateId { get; set; }

        [AttributeDescriptor("item_version_checklist_template_id", true)]
        public System.Int64 ItemVersionChecklistTemplateId { get; set; }
        public long? DependentVersionChecklistTemplateId
        {
            get
            {
                long? id = null;
                if (DependentBlockVersionChecklistTemplate != null)
                {
                    id = DependentBlockVersionChecklistTemplate.VersionChecklistTemplateId;
                }
                else if (DependentItemVersionChecklistTemplate != null)
                {
                    id = DependentItemVersionChecklistTemplate.VersionChecklistTemplateId;
                }

                return id;
            }
        }

        public ItemVersionChecklistTemplate DependentItemVersionChecklistTemplate { get => GetManyToOneData<ItemVersionChecklistTemplate>().Result; }
        public BlockVersionChecklistTemplate DependentBlockVersionChecklistTemplate { get => GetManyToOneData<BlockVersionChecklistTemplate>().Result; }


        #endregion

        #region Validation


        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                List<DomainError> erros = new List<DomainError>();

                if (!newRecord && DependencyItemVersionChecklistTemplateId <= 0)
                {
                    erros.Add(new DomainError("dependency_item_version_checklist_template_id", "InvalidDependencyItemVersionChecklistTemplateIdIdentifier"));
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


        public async Task Update(long? actionUserId, System.Int64? dependentBlockVersionChecklistTemplateId, System.Int64? dependentItemVersionChecklistTemplateId, System.Int64 itemVersionChecklistTemplateId)
        {
            try
            {
                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();
                DependentBlockVersionChecklistTemplateId = dependentBlockVersionChecklistTemplateId;
                DependentItemVersionChecklistTemplateId = dependentItemVersionChecklistTemplateId;
                ItemVersionChecklistTemplateId = itemVersionChecklistTemplateId;


                using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    if (await Validate(false))
                    {
                        await Update();

                        auditTrail.AddRecord("AT_DependencyItemVersionChecklistTemplateUpdated", DependencyItemVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
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
                throw new DomainException("DependencyItemVersionChecklistTemplateDataUpdateError", ex);
            }
        }

        #endregion

        #region User Code



        #endregion
    }
}