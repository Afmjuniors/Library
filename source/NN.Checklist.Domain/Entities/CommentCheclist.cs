
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
    public class CommentCheclist : DomainBase<CommentCheclist, ICommentCheclistRepository<CommentCheclist, System.Int64>, System.Int64>
    {

        #region Constructors

        public CommentCheclist()
        {

        }
        
        public CommentCheclist(long? actionUserId, System.Int64 checklistId, System.String comments, System.Int64 creationTimestamp, System.Int64 creationUserId, System.String stamp)
        {

            var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();

                        ChecklistId = checklistId; 
            Comments = comments; 
            CreationTimestamp = creationTimestamp; 
            CreationUserId = creationUserId; 
            Stamp = stamp; 


            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Validate(true).Result)
                {
                    Insert().Wait();

                    auditTrail.AddRecord("AT_CommentCheclistInserted", CommentChecklistId, EnumSystemFunctionality.Checklists, actionUserId);
                }
                tran.Complete();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("comment_checklist_id", true, EnumValueRanges.Positive)]
        public System.Int64 CommentChecklistId { get; internal set; }

        [AttributeDescriptor("checklist_id", true)] 
        public System.Int64 ChecklistId { get; set; }

        [AttributeDescriptor("comments", true)] 
        public System.String Comments { get; set; }

        [AttributeDescriptor("creation_timestamp", true)] 
        public System.Int64 CreationTimestamp { get; set; }

        [AttributeDescriptor("creation_user_id", true)] 
        public System.Int64 CreationUserId { get; set; }

        [AttributeDescriptor("stamp", true)] 
        public System.String Stamp { get; set; }

        public Checklist Checklist { get => GetManyToOneData<Checklist>().Result; }

        public User CreationUser { get => GetManyToOneData<User>().Result; }



        #endregion

        #region Validation

        
        public async Task<bool> Validate(bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                List<DomainError> erros = new List<DomainError>();

                if (!newRecord && CommentChecklistId <= 0)
                {
                    erros.Add(new DomainError("comment_checklist_id", "InvalidCommentChecklistIdIdentifier"));
                }
                else
                {
                                        if (String.IsNullOrEmpty(Comments))
                    {
                        erros.Add(new DomainError("comments", "CommentsInvalid"));
                    }
                    
                    if (Comments != null && Comments.Length > 5000)
                    {
                        erros.Add(new DomainError("comments", "CommentsInvalidSize"));
                    }
                    
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
        
        
        public async Task Update(long? actionUserId, System.Int64 checklistId, System.String comments, System.Int64 creationTimestamp, System.Int64 creationUserId, System.String stamp)
        {
            try
            {
                var auditTrail = ObjectFactory.GetSingleton<IAuditTrailService>();
                            ChecklistId = checklistId; 
            Comments = comments; 
            CreationTimestamp = creationTimestamp; 
            CreationUserId = creationUserId; 
            Stamp = stamp; 


                using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                { 
                    if (await Validate(false))
                    {
                        await Update();

                        auditTrail.AddRecord("AT_CommentCheclistUpdated", CommentChecklistId, EnumSystemFunctionality.Checklists, actionUserId);
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
                throw new DomainException("CommentCheclistDataUpdateError", ex);
            }
        }

        #endregion

        #region User Code
                    
        

        #endregion
    }
}