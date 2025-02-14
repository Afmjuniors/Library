
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
using System.Linq;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:11

#endregion

namespace NN.Checklist.Domain.Entities
{
    public class ItemVersionChecklistTemplate : DomainBase<ItemVersionChecklistTemplate, IItemVersionChecklistTemplateRepository<ItemVersionChecklistTemplate, System.Int64>, System.Int64>
    {

        #region Constructors

        public ItemVersionChecklistTemplate()
        {

        }

        public ItemVersionChecklistTemplate(long? actionUserId, System.Int64 blockVersionChecklistTemplateId, EnumItemType itemTypeId, System.Int64? optionFieldVersionChecklistTemplateId, System.String optionsTitle, System.Int32 position, System.String title, System.Int64 versionChecklistTemplateId)
        {

            var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

            BlockVersionChecklistTemplateId = blockVersionChecklistTemplateId;
            ItemTypeId = itemTypeId;
            OptionFieldVersionChecklistTemplateId = optionFieldVersionChecklistTemplateId;
            OptionsTitle = optionsTitle;
            Position = position;
            Title = title;
            VersionChecklistTemplateId = versionChecklistTemplateId;


            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Validate(true).Result)
                {
                    Insert().Wait();

                    auditTrail.AddRecord("AT_ItemVersionChecklistTemplateInserted", ItemVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
                }
                tran.Complete();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("item_version_checklist_template_id", true, EnumValueRanges.Positive)]
        public System.Int64 ItemVersionChecklistTemplateId { get; internal set; }

        [AttributeDescriptor("block_version_checklist_template_id", true)]
        public System.Int64 BlockVersionChecklistTemplateId { get; set; }

        [AttributeDescriptor("item_type_id", true)]
        public EnumItemType ItemTypeId { get; set; }

        [AttributeDescriptor("option_field_version_checklist_template_id", false)]
        public System.Int64? OptionFieldVersionChecklistTemplateId { get; set; }

        [AttributeDescriptor("options_title", false)]
        public System.String OptionsTitle { get; set; }

        [AttributeDescriptor("position", true)]
        public System.Int32 Position { get; set; }

        [AttributeDescriptor("title", true)]
        public System.String Title { get; set; }

        [AttributeDescriptor("version_checklist_template_id", true)]
        public System.Int64 VersionChecklistTemplateId { get; set; }
        public bool IsDisabled { get; set; } = false;

        public string AbsolutePositionString
        {
            get
            {
                BlockVersionChecklistTemplate parentBlock = BlockVersionChecklistTemplate.Repository.Get(BlockVersionChecklistTemplateId).Result;

                var absolutePosition = string.Empty;
                if (parentBlock != null)
                {
                    absolutePosition += parentBlock.AbsolutePositionString;
                }
                absolutePosition += "." + Position.ToString();


                return absolutePosition.TrimStart('.');

            }
        }

        public OptionFieldVersionChecklistTemplate OptionFieldVersionChecklistTemplate { get => GetManyToOneData<OptionFieldVersionChecklistTemplate>().Result; }
        public IList<DependencyItemVersionChecklistTemplate>? DependencyItemVersionChecklistTemplate { get => GetOneToManyData<DependencyItemVersionChecklistTemplate>().Result; }
        public IList<OptionItemVersionChecklistTemplate>? OptionItemsVersionChecklistTemplate { get => GetOneToManyData<OptionItemVersionChecklistTemplate>().Result; }

      

        #endregion

        #region Validation


        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                List<DomainError> erros = new List<DomainError>();

                if (!newRecord && ItemVersionChecklistTemplateId <= 0)
                {
                    erros.Add(new DomainError("item_version_checklist_template_id", "InvalidItemVersionChecklistTemplateIdIdentifier"));
                }
                else
                {
                    if (String.IsNullOrEmpty(Title))
                    {
                        erros.Add(new DomainError("title", "TitleInvalid"));
                    }

                    if (Title != null && Title.Length > 500)
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


        public async Task Update(long? actionUserId, System.Int64 blockVersionChecklistTemplateId, EnumItemType itemTypeId, System.Int64? optionFieldVersionChecklistTemplateId, System.String optionsTitle, System.Int32 position, System.String title, System.Int64 versionChecklistTemplateId)
        {
            try
            {
                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

                BlockVersionChecklistTemplateId = blockVersionChecklistTemplateId;
                ItemTypeId = itemTypeId;
                OptionFieldVersionChecklistTemplateId = optionFieldVersionChecklistTemplateId;
                OptionsTitle = optionsTitle;
                Position = position;
                Title = title;
                VersionChecklistTemplateId = versionChecklistTemplateId;

                using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    if (await Validate(false))
                    {
                        await Update();

                        auditTrail.AddRecord("AT_ItemVersionChecklistTemplateUpdated", ItemVersionChecklistTemplateId, EnumSystemFunctionality.Checklists, actionUserId);
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
                throw new DomainException("ItemVersionChecklistTemplateDataUpdateError", ex);
            }
        }

        #endregion

        #region User Code

        public void CheckAvailability(IList<ItemChecklist>? items, IList<BlockVersionChecklistTemplate> blocksChecklistTemplate, string keyValue, EnumFieldDataType? keyType)
        {
            try
            {

                IsDisabled = CheckChecklistDependecy(keyValue,  keyType) || CheckBlockDependency(blocksChecklistTemplate, keyValue, keyType) || CheckItemDependency(items, keyValue, keyType);

            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
        private bool CheckChecklistDependecy(string keyValue, EnumFieldDataType? keyType)
        {
            try
            {
                var checklistDependecies = new List<DependencyItemVersionChecklistTemplate>();
                if (DependencyItemVersionChecklistTemplate != null)
                {
                    checklistDependecies.AddRange(DependencyItemVersionChecklistTemplate.ToList().Where(x => x.DependentVersionChecklistTemplateId.HasValue));

                }
                
                if (checklistDependecies.Any())
                {
                    foreach (var checklistDependecy in checklistDependecies)
                    {

                        if (string.IsNullOrEmpty(keyValue))
                        {
                            return true;
                        }
                        var checklist = Checklist.Repository.GetChecklistByKeyValue(keyValue, (long)checklistDependecy.DependentVersionChecklistTemplateId, keyType).Result;
                        checklist.CheckAvailability();

                        if (!checklist.IsCompleted)
                        {
                            return true;
                        }
                    }
                }



                return false;


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private bool CheckItemDependency(IList<ItemChecklist>? items, string keyValue, EnumFieldDataType? keyType)
        {
            try
            {
                if (DependencyItemVersionChecklistTemplate == null)
                {
                    return false;
                }

                var itemsDependencies = DependencyItemVersionChecklistTemplate.ToList().Where(x => x.DependentItemVersionChecklistTemplateId.HasValue);
                if (itemsDependencies.Any())
                {
                    foreach (var itemDependecy in itemsDependencies)
                    {
                        if (itemDependecy.DependentVersionChecklistTemplateId == VersionChecklistTemplateId)
                        {

                            if (items == null)
                            {
                                return true;
                            }

                            var item = items.Where(x => x.ItemVersionchecklistTemplate.ItemVersionChecklistTemplateId == itemDependecy.DependentItemVersionChecklistTemplateId).OrderByDescending(x => x.CreationTimestamp).FirstOrDefault();

                            if (item==null || (bool)item.IsRejected)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(keyValue))
                            {
                                return true;
                            }
                            var checklist = Checklist.Repository.GetChecklistByKeyValue(keyValue, VersionChecklistTemplateId, keyType).Result;

                            if (checklist.Items == null)
                            {
                                return true;
                            }
                            var itemCk = checklist.Items.Where(x => x.ItemVersionchecklistTemplate.ItemVersionChecklistTemplateId == itemDependecy.DependentItemVersionChecklistTemplateId);

                            if (!itemCk.Any())
                            {
                                return true;
                            }
                        }


                    }
                }



                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private bool CheckBlockDependency(IList<BlockVersionChecklistTemplate> blocksChecklistTemplate, string keyValue, EnumFieldDataType? keyType)
        {
            try
            {
                if (DependencyItemVersionChecklistTemplate == null)
                {
                    return false;
                }


                var blocksDependencies = DependencyItemVersionChecklistTemplate.ToList().Where(x => x.DependentBlockVersionChecklistTemplateId.HasValue);
                if (blocksDependencies.Any())
                {

                    foreach (var blockD in blocksDependencies)
                    {
                        var blockToCheck = blocksChecklistTemplate.Where(x => x.BlockVersionChecklistTemplateId == blockD.DependentBlockVersionChecklistTemplateId).FirstOrDefault();

                        if (blockToCheck.VersionChecklistTemplateId == VersionChecklistTemplateId)
                        {

                            if (blockToCheck != null)
                            {
                                if ((!blockToCheck.IsCompleted.HasValue))
                                {

                                    return true;
                                }
                                if (!(bool)blockToCheck.IsCompleted)
                                {
                                    return true;
                                }

                            }
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(keyValue))
                            {
                                return true;
                            }
                            var checklist = Checklist.Repository.GetChecklistByKeyValue(keyValue, VersionChecklistTemplateId, keyType).Result;
                            checklist.CheckAvailability();
                            var blockDiff = checklist.VersionChecklistTemplate.BlocksChecklistTemplate.Where(x => x.BlockVersionChecklistTemplateId == blockToCheck.BlockVersionChecklistTemplateId).FirstOrDefault();
                            if (blockDiff != null)
                            {
                                if ((!blockToCheck.IsCompleted.HasValue))
                                {

                                    return true;
                                }
                                if (!(bool)blockToCheck.IsCompleted)
                                {
                                    return true;
                                }
                            }
                        }
                    }

                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion
    }
}