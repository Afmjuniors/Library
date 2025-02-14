
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
using NN.Checklist.Domain.Entities.Interfaces;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:12

#endregion

namespace NN.Checklist.Domain.Entities
{
    public class DependencyBlockVersionChecklistTemplate : DomainBase<DependencyBlockVersionChecklistTemplate, IDependencyBlockVersionChecklistTemplateRepository<DependencyBlockVersionChecklistTemplate, System.Int64>, System.Int64>, IDependecy
    {

        #region Constructors

        public DependencyBlockVersionChecklistTemplate()
        {

        }

        public DependencyBlockVersionChecklistTemplate(long? actionUserId, System.Int64 blockVersionChecklistTemplateId, System.Int64? dependentBlockVersionChecklistTemplateId, System.Int64? dependentItemVersionChecklistTemplateId)
        {

            var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

            BlockVersionChecklistTemplateId = blockVersionChecklistTemplateId;
            DependentBlockVersionChecklistTemplateId = dependentBlockVersionChecklistTemplateId;
            DependentItemVersionChecklistTemplateId = dependentItemVersionChecklistTemplateId;


            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Validate(true).Result)
                {
                    Insert().Wait();

                    auditTrail.AddRecord("AT_DependencyBlockVersionChecklistTemplateInserted", DependencyBlockVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
                }
                tran.Complete();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("dependency_block_version_checklist_template_id", true, EnumValueRanges.Positive)]
        public System.Int64 DependencyBlockVersionChecklistTemplateId { get; internal set; }

        [AttributeDescriptor("block_version_checklist_template_id", true)]
        public System.Int64 BlockVersionChecklistTemplateId { get; set; }

        [AttributeDescriptor("dependent_block_version_checklist_template_id", false)]
        public System.Int64? DependentBlockVersionChecklistTemplateId { get; set; }

        [AttributeDescriptor("dependent_item_version_checklist_template_id", false)]
        public System.Int64? DependentItemVersionChecklistTemplateId { get; set; }
        public string DependentString
        {
            get
            {

                var dependentString = string.Empty;

                if (DependentItemVersionChecklistTemplate != null)
                {
                    dependentString = DependentItemVersionChecklistTemplate.AbsolutePositionString;
                }
                else
                if (DependentBlockVersionChecklistTemplate != null)
                {
                    dependentString = DependentBlockVersionChecklistTemplate.AbsolutePositionString;
                }

                return dependentString;

            }
        }
        [AttributeDescriptor("dependent_version_checklist_template_id", false)]
        public long? DependentVersionChecklistTemplateId { get; set; }

        public VersionChecklistTemplate DependentVersionChecklistTemplate { get => GetManyToOneData<VersionChecklistTemplate>().Result; }
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

                if (!newRecord && DependencyBlockVersionChecklistTemplateId <= 0)
                {
                    erros.Add(new DomainError("dependency_block_version_checklist_template_id", "InvalidDependencyBlockVersionChecklistTemplateIdIdentifier"));
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


        public async Task Update(long? actionUserId, System.Int64 blockVersionChecklistTemplateId, System.Int64? dependentBlockVersionChecklistTemplateId, System.Int64? dependentItemVersionChecklistTemplateId)
        {
            try
            {
                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();
                BlockVersionChecklistTemplateId = blockVersionChecklistTemplateId;
                DependentBlockVersionChecklistTemplateId = dependentBlockVersionChecklistTemplateId;
                DependentItemVersionChecklistTemplateId = dependentItemVersionChecklistTemplateId;


                using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    if (await Validate(false))
                    {
                        await Update();

                        auditTrail.AddRecord("AT_DependencyBlockVersionChecklistTemplateUpdated", DependencyBlockVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
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
                throw new DomainException("DependencyBlockVersionChecklistTemplateDataUpdateError", ex);
            }
        }

        #endregion

        #region User Code



        #endregion
    }
}