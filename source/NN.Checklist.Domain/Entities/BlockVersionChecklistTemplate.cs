
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
using static iTextSharp.text.pdf.AcroFields;
using static iTextSharp.text.pdf.events.IndexEvents;

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
        public BlockVersionChecklistTemplate ParentBlock { get => GetManyToOneData<BlockVersionChecklistTemplate>().Result; }
        public bool? IsCompleted { get; set; }
        public bool IsDisabled { get; set; }

        public string AbsolutePositionString
        {
            get
            {
                var absolutePosition = string.Empty;
                if (ParentBlock != null)
                {
                    absolutePosition += ParentBlock.AbsolutePositionString;
                }
                absolutePosition += "." + Position.ToString();


                return absolutePosition.TrimStart('.');

            }
        }


        public IList<DependencyBlockVersionChecklistTemplate>? DependentBlockVersionChecklistTemplate { get => GetOneToManyData<DependencyBlockVersionChecklistTemplate>().Result; }

        private IList<ItemVersionChecklistTemplate> _itemsChecklistsTemplate;

        public IList<ItemVersionChecklistTemplate> ItemsChecklistsTemplate
        {
            get
            {
                if (_itemsChecklistsTemplate == null)
                {
                    // Carregue os dados apenas quando necessário
                    _itemsChecklistsTemplate = GetOneToManyData<ItemVersionChecklistTemplate>().Result;
                }
                return _itemsChecklistsTemplate;
            }
            set
            {
                _itemsChecklistsTemplate = value;
            }
        }
        public IList<BlockVersionChecklistTemplateDTO> Blocks { get; set; }




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

        public void CheckAvailability(IList<ItemChecklist>? items, IList<BlockVersionChecklistTemplate> blocksChecklistTemplate, string keyValue, List<BlockVersionChecklistTemplate> blocks)
        {
            try
            {
                IsCompleted = IsBlockCompleted(items, blocks);
                if (IsCompleted.Value)
                {
                    return;
                }
                bool hasParentDependency = false;
                if (ParentBlockVersionChecklistTemplateId.HasValue)
                {
                    hasParentDependency = ParentBlock.DependentBlockVersionChecklistTemplate != null;

                }
                if (DependentBlockVersionChecklistTemplate != null || hasParentDependency)
                {
                    IsDisabled = CheckBlockDependency(blocksChecklistTemplate, keyValue) || CheckItemDependency(items, keyValue);
                }

                if (ItemsChecklistsTemplate != null)
                {

                    List<ItemVersionChecklistTemplate> lst = new List<ItemVersionChecklistTemplate>();
                    foreach (var item in ItemsChecklistsTemplate)
                    {
                        if (IsDisabled)
                        {
                            item.IsDisabled = true;
                        }
                        else
                        {
                            item.CheckAvailability(items, blocksChecklistTemplate, keyValue);

                        }

                        lst.Add(item);
                    }
                    _itemsChecklistsTemplate = lst;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private bool CheckItemDependency(IList<ItemChecklist>? items, string keyValue)
        {
            try
            {
                var itemsDependencies = new List<DependencyBlockVersionChecklistTemplate>();
                if (DependentBlockVersionChecklistTemplate != null)
                {
                    itemsDependencies.AddRange(DependentBlockVersionChecklistTemplate.ToList().Where(x => x.DependentItemVersionChecklistTemplateId.HasValue));

                }
                if (ParentBlock != null && ParentBlock.DependentBlockVersionChecklistTemplate != null)
                {
                    itemsDependencies.AddRange(ParentBlock.DependentBlockVersionChecklistTemplate.ToList().Where(x => x.DependentItemVersionChecklistTemplateId.HasValue));
                }


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

                            if (item == null || (bool)item.IsRejected)
                            {
                                return true;
                            }

                        }
                        else
                        {
                            if (string.IsNullOrEmpty(keyValue))
                            {
                                return true;
                            }
                            var checklist = Checklist.Repository.GetChecklistByKeyValue(keyValue, VersionChecklistTemplateId).Result;

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
        private bool CheckBlockDependency(IList<BlockVersionChecklistTemplate> blocksChecklistTemplate, string keyValue)
        {
            var blocksDependencies = new List<DependencyBlockVersionChecklistTemplate>();
            var listHasparent = blocksChecklistTemplate.Where(x => x.ParentBlockVersionChecklistTemplateId.HasValue);
            if (DependentBlockVersionChecklistTemplate != null)
            {
                blocksDependencies.AddRange(DependentBlockVersionChecklistTemplate.ToList().Where(x => x.DependentBlockVersionChecklistTemplateId.HasValue));

            }
            if (ParentBlock != null && ParentBlock.DependentBlockVersionChecklistTemplate != null)
            {
                blocksDependencies.AddRange(ParentBlock.DependentBlockVersionChecklistTemplate.ToList().Where(x => x.DependentBlockVersionChecklistTemplateId.HasValue));
            }

            if (blocksDependencies.Any())
            {

                foreach (var blockD in blocksDependencies)
                {
                    var blockToCheck = blocksChecklistTemplate.Where((x) =>
                    {

                        if (x.ParentBlockVersionChecklistTemplateId.HasValue)
                        {
                            return x.BlockVersionChecklistTemplateId == blockD.BlockVersionChecklistTemplateId || x.ParentBlock.BlockVersionChecklistTemplateId == blockD.BlockVersionChecklistTemplateId;

                        }
                        else
                        {
                            return x.BlockVersionChecklistTemplateId == blockD.BlockVersionChecklistTemplateId;
                        }
                    }).FirstOrDefault();
                    if (blockToCheck == null)
                    {
                        return false;
                    }
                    if (blockD.DependentVersionChecklistTemplateId == VersionChecklistTemplateId)
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
                        var checklist = Checklist.Repository.GetChecklistByKeyValue(keyValue, (long)blockD.DependentVersionChecklistTemplateId).Result;
                        checklist.CheckAvailability();


                        var blockDiff = checklist.VersionChecklistTemplate.BlocksChecklistTemplate.Where(x => blockToCheck.DependentBlockVersionChecklistTemplate.Any(y => y.DependencyBlockVersionChecklistTemplateId == x.BlockVersionChecklistTemplateId)).FirstOrDefault();
                        if (blockDiff != null)
                        {
                            if ((!blockDiff.IsCompleted.HasValue))
                            {

                                return true;
                            }
                            if (!(bool)blockDiff.IsCompleted)
                            {
                                return true;
                            }
                        }

                    }
                }

            }
            return false;
        }
        public bool IsBlockCompleted(IList<ItemChecklist>? items, List<BlockVersionChecklistTemplate> blocks)
        {
            try
            {


                if (items == null)
                {
                    return false;
                }
                if (ItemsChecklistsTemplate == null)
                {
                    return true;
                }
                var _block = blocks.Where(x => x.ParentBlockVersionChecklistTemplateId == BlockVersionChecklistTemplateId).FirstOrDefault();

                if (_block != null)
                {
                                        
                    var isCompleted = _block.IsBlockCompleted(items, blocks);
                    if (!isCompleted)
                    {
                        return false;
                    }

                }
                if (items.Any())
                {
                    foreach (var item in ItemsChecklistsTemplate)
                    {

                        var signatures = items.Where(x => x.ItemVersionchecklistTemplate.ItemVersionChecklistTemplateId == item.ItemVersionChecklistTemplateId).OrderByDescending(x => x.CreationTimestamp).FirstOrDefault();


                        if (signatures == null || !signatures.IsRejected.HasValue || signatures.IsRejected.Value)
                        {
                            return false;
                        }

                    }
                    return true;

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