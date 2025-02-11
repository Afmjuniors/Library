
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
using static iTextSharp.text.pdf.AcroFields;
using static iTextSharp.text.pdf.events.IndexEvents;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.DTO;

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

        public VersionChecklistTemplate? DependentVersionChecklistTemplate
        {
            get
            {


                foreach (var block in BlocksChecklistTemplate)
                {

                    if (block.DependentBlockVersionChecklistTemplate != null)
                    {
                        foreach (var blockD in block.DependentBlockVersionChecklistTemplate)
                        {
                            if (blockD.DependentVersionChecklistTemplateId != VersionChecklistTemplateId)
                            {

                                return Get((long)blockD.DependentVersionChecklistTemplateId).Result;
                            }
                        }
                    }
                    if (block.ItemsChecklistsTemplate == null)
                    {
                        return null;
                    }
                    foreach (var item in block.ItemsChecklistsTemplate)
                    {
                        if (item.DependencyItemVersionChecklistTemplate == null)
                        {
                            return null;
                        }
                        foreach (var itemD in item.DependencyItemVersionChecklistTemplate)
                        {
                            if (itemD.DependentVersionChecklistTemplateId != VersionChecklistTemplateId)
                            {

                                return Get((long)itemD.DependentVersionChecklistTemplateId).Result;
                            }

                        }

                    }
                }
                return null;
            }
        }

        public List<FieldVersionChecklistTemplate> FieldsVersionChecklistsTemplate { get => GetOneToManyData<FieldVersionChecklistTemplate>().Result.OrderBy(x => x.Position).ToList(); set { } }


        public User CreationUser { get => GetManyToOneData<User>().Result; }

        private IList<BlockVersionChecklistTemplate> _blocksChecklistTemplate;

        public IList<BlockVersionChecklistTemplate> BlocksChecklistTemplate
        {
            get
            {
                if (_blocksChecklistTemplate == null)
                {
                    _blocksChecklistTemplate = GetOneToManyData<BlockVersionChecklistTemplate>().Result;
                }
                return _blocksChecklistTemplate;
            }
            set
            {
                _blocksChecklistTemplate = value;
            }
        }

        public List<BlockVersionChecklistTemplateDTO> BlocksTree { get => LoadBlocks(); }


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

        public void SetLastPosistionInBlocks()
        {
            foreach (var block in BlocksChecklistTemplate)
            {
                block.SetLastPosition(BlocksTree);
            }
        }

        public void CheckAvailability(IList<ItemChecklist>? items, string? keyValue)
        {
            try
            {
                var blocks = BlocksChecklistTemplate.ToList();
                List<BlockVersionChecklistTemplate> lst = new List<BlockVersionChecklistTemplate>();
                foreach (var block in BlocksChecklistTemplate)
                {
                    block.CheckAvailability(items, BlocksChecklistTemplate, keyValue, blocks);
                    lst.Add(block);
                }
                _blocksChecklistTemplate = lst;
            }


            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool CheckIfCompleted(IList<ItemChecklist>? items)
        {
            try
            {
                var _blocks = BlocksChecklistTemplate.ToList();
                foreach (var block in BlocksChecklistTemplate)
                {
                    var flag = block.IsBlockCompleted(items, _blocks);
                    if (!flag)
                    {
                        return false;
                    }

                }
                return true;


            }


            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<BlockVersionChecklistTemplateDTO> LoadBlocks()
        {
            var rootBlocks = new List<BlockVersionChecklistTemplateDTO>();

            if (BlocksChecklistTemplate != null && BlocksChecklistTemplate.Count > 0)
            {
                var main = BlocksChecklistTemplate.Where(x => x.ParentBlockVersionChecklistTemplateId == null);
                var root = main.TransformList<BlockVersionChecklistTemplateDTO>();


                if (main != null)
                {
                    foreach (var block in root)
                    {


                        block.Blocks = BuildBlocksTree(block);
                    
                        rootBlocks.Add(block);
                        
                    }
                }
            }


            return rootBlocks;
        }

        private List<BlockVersionChecklistTemplateDTO> BuildBlocksTree(BlockVersionChecklistTemplateDTO blockPrev)
        {
            var lst = BlocksChecklistTemplate.Where(x => x.ParentBlockVersionChecklistTemplateId == blockPrev.BlockVersionChecklistTemplateId).OrderBy(x => x.Position).ToList();
            var chlidrenBlocks = lst.TransformList<BlockVersionChecklistTemplateDTO>().ToList();
            if (lst != null && lst.Count > 0)
            {

                foreach (var childBlock in chlidrenBlocks)
                {
              
                        var children = BuildBlocksTree(childBlock);                        
                        childBlock.Blocks = children;

                    
                }

                return chlidrenBlocks;
            }

            return null;
        }

        #endregion
    }
}