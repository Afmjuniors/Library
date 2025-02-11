using iTextSharp.text;
using K4os.Compression.LZ4.Internal;
using NN.Checklist.Domain.Common;
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Request;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.DTO.Response.User;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Entities.Interfaces;
using NN.Checklist.Domain.Services.Specifications;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Data.Paging;
using TDCore.DependencyInjection;
using TDCore.Domain.Exceptions;
using static ICSharpCode.SharpZipLib.Zip.ExtendedUnixData;
using static iTextSharp.text.pdf.AcroFields;
using static iTextSharp.text.pdf.qrcode.Version;
using static NPOI.HSSF.UserModel.HeaderFooter;

namespace NN.Checklist.Domain.Services
{
    public class ChecklistService : ObjectBase, IChecklistService
    {

        public async Task<List<ChecklistTemplateDTO>> ListChecklistTemplate()
        {
            var checklistTeplate = await ChecklistTemplate.Repository.ListAll();

            var dto = checklistTeplate.TransformList<ChecklistTemplateDTO>().ToList();

            return dto;


        }
        public async Task<List<ChecklistResponseDTO>> ListChecklist(long versionChecklistId)
        {
            try
            {



                var listChecklist = await Entities.Checklist.Repository.ListChecklistByVersion(versionChecklistId);

                List<ChecklistResponseDTO> lst = new List<ChecklistResponseDTO>();

                if (listChecklist != null)
                {
                    foreach (var checklist in listChecklist)
                    {

                        var fields = checklist.Fields.Where(x => x.FieldVersionChecklistTemplate.IsKey);
                        string description = "teste";
                        if (fields != null && fields.Any())
                        {
                            description = fields.First().Value;

                        }

                        var dto = new ChecklistResponseDTO()
                        {
                            ChecklistId = checklist.ChecklistId,
                            VersionChecklistTemplateId = checklist.VersionChecklistTemplateId,
                            ChecklistTemplateDescription = checklist.VersionChecklistTemplate.ChecklistTemplate.Description,
                            ChecklistTemplateVersion = checklist.VersionChecklistTemplate.Version,
                            CreationTimestamp = checklist.CreationTimestamp,
                            UpdateTimestamp = checklist.UpdateTimestamp,
                            CreationUser = checklist.CreationUser.Transform<UserDTO>(),
                            Description = description
                        };
                        lst.Add(dto);

                    }
                }

                return lst;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        public async Task<VersionChecklistTemplateDTO> GetLatestCheckList(long checklistId)
        {
            var checklistTeplate = await VersionChecklistTemplate.Repository.GetLatestVersionFromChecklistId(checklistId);
            checklistTeplate.SetLastPosistionInBlocks();
            var dto = checklistTeplate.Transform<VersionChecklistTemplateDTO>();

            return dto;


        }

        public async Task<List<VersionChecklistTemplateDTO>> ListChecklistVersions(long checklistId)
        {
            var checklistTeplate = await VersionChecklistTemplate.Repository.ListVersionFromChecklistTemplateId(checklistId);
            IList<VersionChecklistTemplateDTO> dto = new List<VersionChecklistTemplateDTO>();
            if (checklistTeplate != null)
            {
                dto = checklistTeplate.TransformList<VersionChecklistTemplateDTO>();

            }


            return dto.ToList();
        }

        public async Task<ChecklistDTO> CreateUpdateChecklist(AuthenticatedUserDTO user, ChecklistDTO obj)
        {
            long actionUserId = user.UserId;
            long? checklistId = obj.ChecklistId;
            if (!checklistId.HasValue)
            {
                var checklist = new Entities.Checklist(actionUserId, obj.VersionChecklistTemplateId);
                checklistId = checklist.ChecklistId;

            }

            await CreateFieldChecklist(actionUserId, (long)checklistId, obj.Fields);
            await CreateItemChecklist(actionUserId, (long)checklistId, obj.Items);


            var newChelist = await Entities.Checklist.Repository.Get((long)checklistId);
            newChelist.VersionChecklistTemplate.SetLastPosistionInBlocks();

            var dto = newChelist.Transform<ChecklistDTO>();
            return dto;

        }

        private async Task CreateItemChecklist(long actionUserId, long checklistId, List<ItemChecklistDTO> items)
        {
            if (items == null)
            {
                return;
            }
            foreach (var item in items)
            {
                if (!item.ItemChecklistId.HasValue)
                {
                    //TODO: Verificar os Comments
                    var itemChecklist = new ItemChecklist(actionUserId, checklistId, item.Comments, item.ItemVersionChecklistTemplateId, item.Stamp);

                }
                else
                {
                    var itemFound = await ItemChecklist.Repository.Get((long)item.ItemChecklistId);
                    await itemFound.Update(actionUserId, item.ChecklistId, item.Comments, item.CreationTimestamp, item.CreationUserId, item.ItemVersionChecklistTemplateId, item.Stamp);
                }

            }
        }
        private async Task CreateFieldChecklist(long actionUserId, long checklistId, List<FieldChecklistDTO> fields)
        {
            if (fields == null)
            {
                return;
            }
            foreach (var item in fields)
            {
                if (!item.FieldChecklistId.HasValue)
                {

                    var field = new FieldChecklist(actionUserId, (long)checklistId, DateTime.Now, actionUserId, item.FieldVersionChecklistTemplateId, item.OptionFieldVersionChecklistTemplateId, null, null, item.Value);
                }
                else
                {
                    var fieldFound = await FieldChecklist.Repository.Get((long)item.FieldChecklistId);
                    if (fieldFound.Value != item.Value)
                    {
                        await fieldFound.Update(actionUserId, item.ChecklistId, item.CreationTimestamp, item.CreationUserId, item.FieldVersionChecklistTemplateId, item.OptionFieldVersionChecklistTemplateId, DateTime.Now, actionUserId, item.Value);
                    }
                }
            }
        }

        public async Task<PageMessage<ChecklistDTO>> Search(AuthenticatedUserDTO auth, ChecklistPageMessage pageMessage)
        {
            try
            {
                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

                var res = await Entities.Checklist.Repository.Search(pageMessage);
                var accessControlService = ObjectFactory.GetSingleton<IAccessControlService>();


                if (res.Entities == null)
                {

                    return res;
                }


                res.Entities = CheckAvabiality(res.Entities);


                foreach (var entity in res.Entities)
                {
                    if (entity.Items != null)
                    {
                        foreach (var item1 in entity.Items)
                        {
                            item1.Signature = await accessControlService.ReadSignature(item1.Stamp);
                        }
                    }


                }

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ChecklistDTO> SignItem(AuthenticatedUserDTO auth, ItemChecklistDTO item)
        {

            Entities.Checklist checklist;
            var accessControlService = ObjectFactory.GetSingleton<IAccessControlService>();
            bool isNew = true;
            if (item.ChecklistId > 0)
            {
                checklist = await Entities.Checklist.Repository.Get((long)item.ChecklistId);
            }
            else
            {
                checklist = new Entities.Checklist(auth.UserId, (long)item.VersionChecklistTemplateId);
            }
            if (checklist.Items != null)
            {
                isNew = !checklist.Items.Any(x => x.ItemVersionchecklistTemplateId == item.ItemVersionChecklistTemplateId);
            }

            var newItem = new ItemChecklist(auth.UserId, checklist.ChecklistId, item.Comments, DateTime.Now, auth.UserId, item.ItemVersionChecklistTemplateId, item.Stamp);
            if (item.OptionsItemsChecklist != null)
            {
                foreach (var optionItem in item.OptionsItemsChecklist)
                {
                    var option = new OptionItemChecklist(auth.UserId, DateTime.Now, auth.UserId, newItem.ItemChecklistId, optionItem.OptionItemVersionChecklistTemplateId);
                    if (optionItem.CancelledItemsVersionChecklistTemplate != null)
                    {
                        foreach (var cancelledItem in optionItem.CancelledItemsVersionChecklistTemplate)
                        {
                            if (checklist.Items != null)
                            {
                                var itemToReject = checklist.Items
                                    .Where(x => x.ItemVersionchecklistTemplateId == cancelledItem.TargetItemVersionChecklistTemplateId)
                                    .OrderByDescending(x => x.CreationTimestamp)
                                    .FirstOrDefault();
                                if (itemToReject != null)
                                {
                                    await itemToReject.RejectItem();

                                }

                            }
                        }
                    }
                }
            }
            if (!isNew)
            {

             await   checklist.CheckBlockAndItemDependencyForRejection(newItem.ItemVersionchecklistTemplate.BlockVersionChecklistTemplateId, newItem.ItemVersionchecklistTemplateId);

            }


            checklist.CheckAvailability();
            var dto = checklist.Transform<ChecklistDTO>();
            foreach (var item1 in dto.Items)
            {
                item1.Signature = await accessControlService.ReadSignature(item1.Stamp);
            }

            return dto;
        }



        public async Task<List<HistorySignatureDTO>> ListAllSignuture(AuthenticatedUserDTO auth, long checklistId, long itemTemplateId)
        {
            try
            {


                var accessControlService = ObjectFactory.GetSingleton<IAccessControlService>();
                var res = await ItemChecklist.Repository.ListAllItensByChecklistIdAndIdTemplate(checklistId, itemTemplateId);
                List<HistorySignatureDTO> lst = new List<HistorySignatureDTO>();
                foreach (var item in res)
                {
                    var signature = await accessControlService.ReadSignature(item.Stamp);
                    signature.Comments = item.Comments;
                    var history = new HistorySignatureDTO()
                    {
                        Signature = signature,
                        IsRejected = item.IsRejected,
                        OptionsSelected = item.OptionsItemsChecklist != null ? item.OptionsItemsChecklist.TransformList<OptionItemChecklistDTO>().ToList() : [],
                        OptionsAvalible = item.ItemVersionchecklistTemplate.OptionItemsVersionChecklistTemplate != null ? item.ItemVersionchecklistTemplate.OptionItemsVersionChecklistTemplate.TransformList<OptionItemVersionChecklistTemplateDTO>().ToList() : []
                    };
                    lst.Add(history);

                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private List<ChecklistDTO> CheckAvabiality(IEnumerable<ChecklistDTO> entities)
        {
            try
            {



                List<ChecklistDTO> ckLst = new List<ChecklistDTO>();
                foreach (var entity in entities)
                {
                    var et = entity.Transform<Entities.Checklist>();
                    et.CheckAvailability();
                    
                    var etDTO = et.Transform<ChecklistDTO>();
                    ckLst.Add(etDTO);
                }

                    return ckLst;
            }
            catch (Exception)
            {

                throw;
            }
        }

   

    }
}
