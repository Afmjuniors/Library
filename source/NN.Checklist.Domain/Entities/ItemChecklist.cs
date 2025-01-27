
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
using System.Collections;
using System.Xml.Linq;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 17/12/2024 10:10:10

#endregion

namespace NN.Checklist.Domain.Entities
{
    public class ItemChecklist : DomainBase<ItemChecklist, IItemChecklistRepository<ItemChecklist, System.Int64>, System.Int64>
    {

        #region Constructors

        public ItemChecklist()
        {

        }

        public ItemChecklist(long? actionUserId, System.Int64 checklistId, System.String comments, System.DateTime creationTimestamp, System.Int64 creationUserId, System.Int64 itemVersionchecklistTemplateId, System.String stamp)
        {

            var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

            ChecklistId = checklistId;
            Comments = comments;
            CreationTimestamp = creationTimestamp;
            CreationUserId = creationUserId;
            ItemVersionchecklistTemplateId = itemVersionchecklistTemplateId;
            Stamp = stamp;


            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Validate(true).Result)
                {
                    Insert().Wait();

                    auditTrail.AddRecord("AT_ItemChecklistInserted", ItemChecklistId, EnumSystemFunctionality.Checklists, actionUserId);
                }
                tran.Complete();
            }
        }
        public ItemChecklist(long actionUserId, System.Int64 checklistId, System.String comments, System.Int64 itemVersionchecklistTemplateId, System.String stamp)
        {

            var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

            ChecklistId = checklistId;
            Comments = comments;
            CreationTimestamp = DateTime.Now;
            CreationUserId = actionUserId;
            ItemVersionchecklistTemplateId = itemVersionchecklistTemplateId;
            Stamp = stamp;




            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Validate(true).Result)
                {
                    Insert().Wait();

                    auditTrail.AddRecord("AT_ItemChecklistInserted", ItemChecklistId, EnumSystemFunctionality.Checklists, actionUserId);
                }
                tran.Complete();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("item_checklist_id", true, EnumValueRanges.Positive)]
        public System.Int64 ItemChecklistId { get; internal set; }

        [AttributeDescriptor("checklist_id", true)]
        public System.Int64 ChecklistId { get; set; }

        [AttributeDescriptor("comments", false)]
        public System.String Comments { get; set; }

        [AttributeDescriptor("creation_timestamp", true)]
        public System.DateTime CreationTimestamp { get; set; }

        [AttributeDescriptor("creation_user_id", true)]
        public System.Int64 CreationUserId { get; set; }

        [AttributeDescriptor("item_version_checklist_template_id", true)]
        public System.Int64 ItemVersionchecklistTemplateId { get; set; }

        [AttributeDescriptor("stamp", true)]
        public System.String Stamp { get; set; }

        [AttributeDescriptor("is_rejected", false)]
        public bool? IsRejected { get; set; } = false;

        public Checklist Checklist { get => GetManyToOneData<Checklist>().Result; }

        public User CreationUser { get => GetManyToOneData<User>().Result; }

        public IList<OptionItemChecklist> OptionsItemsChecklist { get => GetOneToManyData<OptionItemChecklist>().Result; }
        public ItemVersionChecklistTemplate ItemVersionchecklistTemplate { get => GetManyToOneData<ItemVersionChecklistTemplate>().Result; }



        #endregion

        #region Validation


        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                List<DomainError> erros = new List<DomainError>();

                if (!newRecord && ItemChecklistId <= 0)
                {
                    erros.Add(new DomainError("item_checklist_id", "InvalidItemChecklistIdIdentifier"));
                }
                else
                {
                    if (String.IsNullOrEmpty(Stamp))
                    {
                        erros.Add(new DomainError("stamp", "StampInvalid"));
                    }

                    if (Stamp != null && Stamp.Length > 500)
                    {
                        erros.Add(new DomainError("stamp", "StampInvalidSize"));
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


        public async Task Update(long? actionUserId, System.Int64 checklistId, System.String comments, System.DateTime creationTimestamp, System.Int64 creationUserId, System.Int64 itemVersionchecklistTemplateId, System.String stamp)
        {
            try
            {
                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();
                ChecklistId = checklistId;
                Comments = comments;
                CreationTimestamp = creationTimestamp;
                CreationUserId = creationUserId;
                ItemVersionchecklistTemplateId = itemVersionchecklistTemplateId;
                Stamp = stamp;


                using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    if (await Validate(false))
                    {
                        await Update();

                        auditTrail.AddRecord("AT_ItemChecklistUpdated", ItemChecklistId, EnumSystemFunctionality.Checklists, actionUserId);
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
                throw new DomainException("ItemChecklistDataUpdateError", ex);
            }
        }

        #endregion

        #region User Code
        public async Task RejectItem()
        {
            try
            {

                IsRejected = true;
                await Update();
            }
            catch (DomainException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DomainException("UpdatedItemRejectionError", ex);
            }

        }
        public async Task ApproveItem()
        {
            try
            {

                IsRejected = false;
                await Update();
            }
            catch (DomainException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DomainException("UpdatedItemRejectionError", ex);
            }

        }


        #endregion
    }
}