
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
using NN.Checklist.Domain.DTO;
using System.Linq;
using iTextSharp.text;
using static iTextSharp.text.pdf.AcroFields;

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
        public Checklist(long actionUserId, long versionChecklistTemplateId)
        {

            try
            {
                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();
                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();


                CreationTimestamp = DateTime.Now;
                CreationUserId = actionUserId;
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
            catch (DomainException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DomainException("EX_ChecklistNotCreated", ex);
            }






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
        public bool IsCompleted { get; set; }

        public User CreationUser { get => GetManyToOneData<User>().Result; }

        public User UpdateUser { get => GetManyToOneData<User>().Result; }

        public VersionChecklistTemplate? _versionChecklistTemplate;
        public VersionChecklistTemplate? VersionChecklistTemplate
        {
            get
            {
                if (_versionChecklistTemplate == null)
                {
                    _versionChecklistTemplate = GetManyToOneData<VersionChecklistTemplate>().Result;
                }
                return _versionChecklistTemplate;
            }
            set
            {
                _versionChecklistTemplate = value;
            }
        }
        public IList<ItemChecklist> Items { get => GetOneToManyData<ItemChecklist>().Result; set { } }
        public IList<FieldChecklist> Fields { get => GetOneToManyData<FieldChecklist>().Result; set { } }




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
        public void CheckAvailability()
        {
            try
            {
                string keyValue = "";
                if (Fields != null)
                {
                    keyValue = Fields.Where(x => x.FieldVersionChecklistTemplate.IsKey).FirstOrDefault().Value;

                }

                VersionChecklistTemplate.CheckAvailability(Items, keyValue);
                CheckChecklistIsCompleted();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task CheckBlockAndItemDependencyForRejection(long? blockVersionId, long? itemVersionId)
        {
            await CheckBlockDependencyRejection(blockVersionId, itemVersionId, null);

            await CheckItemDependencyRejection(blockVersionId, itemVersionId, null);




        }
        private async Task CheckItemDependencyRejection(long? blockVersionId, long? itemVersionId, IList<DependencyItemVersionChecklistTemplate> prevItems)
        {
            var itemsToReject = await DependencyItemVersionChecklistTemplate.Repository.ListAllBLocksFromDependentBlockIdOrItemBlockId(blockVersionId, itemVersionId);

            if (prevItems == null || !prevItems.Any())
            {
                prevItems = itemsToReject ?? new List<DependencyItemVersionChecklistTemplate>();
            }
            if (itemsToReject == null || !itemsToReject.Any())
            {
                return;
            }

            foreach (var item in itemsToReject)
            {
                var itemToCheck = Items
                    .Where(x => x.ItemVersionchecklistTemplateId == item.ItemVersionChecklistTemplateId)
                    .OrderByDescending(x => x.CreationTimestamp)
                    .FirstOrDefault();
                if(itemToCheck== null)
                {
                    var a = 1;
                }

                if(itemToCheck!=null)
                await CheckItemDependencyRejection(
                    itemToCheck.ItemVersionchecklistTemplate.BlockVersionChecklistTemplateId,
                    itemToCheck.ItemVersionchecklistTemplateId,
                    itemsToReject);

                var itemToReject = Items?
                    .Where(x => x.ItemVersionchecklistTemplateId == item.ItemVersionChecklistTemplateId)
                    .OrderByDescending(x => x.CreationTimestamp)
                    .FirstOrDefault();

                if (itemToReject != null)
                {
                    await itemToReject.RejectItem();
                }

            }
        }

        private async Task CheckBlockDependencyRejection(long? blockVersionId, long? itemVersionId, IList<DependencyBlockVersionChecklistTemplate> prevBlocks)
        {


            var blocksToReject = await DependencyBlockVersionChecklistTemplate.Repository.ListAllBLocksFromDependentBlockIdOrItemBlockId(blockVersionId, itemVersionId);

            if (prevBlocks == null || !prevBlocks.Any())
            {
                prevBlocks = blocksToReject ?? new List<DependencyBlockVersionChecklistTemplate>();
            }


            if (blocksToReject == null)
            {

                return;
            }

            foreach (var blockToReject in blocksToReject)
            {
                var blks = VersionChecklistTemplate.BlocksChecklistTemplate.Where(x => x.BlockVersionChecklistTemplateId == blockToReject.BlockVersionChecklistTemplateId).FirstOrDefault();
                if (blks != null)
                {
                    //aki n estou pegando o bloco estou pegando o item
                    var blockToCheck = Items
                        .Where(x => x.ItemVersionchecklistTemplate.BlockVersionChecklistTemplateId == blockToReject.BlockVersionChecklistTemplateId)
                        .OrderByDescending(x => x.CreationTimestamp)
                        .FirstOrDefault();

                    var iToReject = blks.ItemsChecklistsTemplate;
                    if(blockToCheck!=null)
                    await CheckBlockDependencyRejection(
                   blockToCheck.ItemVersionchecklistTemplate.BlockVersionChecklistTemplateId,
                   blockToCheck.ItemVersionchecklistTemplateId,
                   blocksToReject);

                    foreach (var item2 in iToReject)
                    {
                        if (Items != null)
                        {

                            var itemToReject = Items
                                .Where(x => x.ItemVersionchecklistTemplateId == item2.ItemVersionChecklistTemplateId)
                                .OrderByDescending(x => x.CreationTimestamp)
                                .FirstOrDefault();
                            if (itemToReject != null)
                            {
                                await itemToReject.RejectItem();

                            }

                        }
                    }

                }
                else
                {

                    var blockCheckList = await BlockVersionChecklistTemplate.Repository.Get(blockToReject.BlockVersionChecklistTemplateId);
                    if (blockCheckList.VersionChecklistTemplateId != VersionChecklistTemplateId)
                    {
                        var a = 1;
                        string keyValue = "";
                        if (Fields != null)
                        {
                            keyValue = Fields.Where(x => x.FieldVersionChecklistTemplate.IsKey).FirstOrDefault().Value;

                        }
                        var checklistOther = await Repository.GetChecklistByKeyValue(keyValue, blockCheckList.VersionChecklistTemplateId);
                        if (checklistOther!=null && checklistOther.Items != null)
                        {
              
                                await checklistOther.CheckBlockAndItemDependencyForRejection(blockVersionId, itemVersionId);
                            
                        }

                    }

                }

            }

        }

        private  void CheckChecklistIsCompleted()
        {
            var blocks = VersionChecklistTemplate.BlocksTree;
            foreach (var block in blocks)
            {
                if(!block.IsCompleted.HasValue || !block.IsCompleted.Value)
                {
                    IsCompleted = false;
                    return;
                }
            }
            IsCompleted = true;
        }
    }



    #endregion
}
