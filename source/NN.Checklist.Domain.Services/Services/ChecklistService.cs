using K4os.Compression.LZ4.Internal;
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Request;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Data.Paging;
using TDCore.DependencyInjection;
using TDCore.Domain.Exceptions;
using static iTextSharp.text.pdf.AcroFields;
using static NPOI.HSSF.UserModel.HeaderFooter;

namespace NN.Checklist.Domain.Services
{
    public class ChecklistService : ObjectBase, IChecklistService
    {

        public async Task<List<ChecklistTemplateDTO>> ListChecklist()
        {
            var checklistTeplate = await ChecklistTemplate.Repository.ListAll();

            var dto = checklistTeplate.TransformList<ChecklistTemplateDTO>().ToList();

            return dto;


        }


        public async Task<VersionChecklistTemplateDTO> GetLatestCheckList(long checklistId)
        {
            var checklistTeplate = await VersionChecklistTemplate.Repository.GetLatestVersionFromChecklistId(checklistId);

            var dto = checklistTeplate.Transform<VersionChecklistTemplateDTO>();

            return dto;


        }

        public async Task<List<VersionChecklistTemplateDTO>> ListChecklistVersions(long checklistId)
        {
            var checklistTeplate = await VersionChecklistTemplate.Repository.ListVersionFromChecklistTemplateId(checklistId);

            var dto = checklistTeplate.TransformList<VersionChecklistTemplateDTO>();

            return dto.ToList();
        }

        public async Task<ChecklistDTO> NewUpdateChecklist(AuthenticatedUserDTO user, ChecklistDTO obj)
        {
            long actionUserId = user.UserId;
            long? checklistId = obj.ChecklistId;
            if (!checklistId.HasValue)
            {
                var checklist = new Entities.Checklist(actionUserId, obj.VersionChecklistTemplateId);
                checklistId = checklist.ChecklistId;

            }
            await CreateUpdateFieldChecklist(actionUserId, (long)checklistId, obj.Fields);
            await CreateUpdateItemChecklist(actionUserId, (long)checklistId, obj.Items);


            var newChelist = await Entities.Checklist.Repository.Get((long)checklistId);


            return newChelist.Transform<ChecklistDTO>();


        }

        private async Task CreateUpdateItemChecklist(long actionUserId, long checklistId, List<ItemChecklistDTO> items)
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
        private async Task CreateUpdateFieldChecklist(long actionUserId, long checklistId, List<FieldChecklistDTO> fields)
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

            var res = await Entities.Checklist.Repository.Search(pageMessage);
            var accessControlService = ObjectFactory.GetSingleton<IAccessControlService>();
            if (res.Entities == null)
            {
                return res;
            }

            foreach (var enty in res.Entities)
            {
                if (enty.Items != null)
                {

                    foreach (var item1 in enty.Items)
                    {


                        item1.Signature = await accessControlService.ReadSignature(item1.Stamp);
                    }
                }


            }
            return res;
        }

        public async Task<ChecklistDTO> SignItem(AuthenticatedUserDTO auth, ItemChecklistDTO item)
        {
            Entities.Checklist checklist;
            var accessControlService = ObjectFactory.GetSingleton<IAccessControlService>();
            if (item.ChecklistId > 0)
            {
                checklist = await Entities.Checklist.Repository.Get((long)item.ChecklistId);
            }
            else
            {
                checklist = new Entities.Checklist(auth.UserId, (long)item.VersionChecklistTemplateId);
            }
            var newItem = new ItemChecklist(auth.UserId, checklist.ChecklistId, item.Comments, DateTime.Now, auth.UserId, item.ItemVersionChecklistTemplateId, item.Stamp);
            var ck = await Entities.Checklist.Repository.Get(checklist.ChecklistId);
            var dto = ck.Transform<ChecklistDTO>();
            foreach (var item1 in dto.Items)
            {
                item1.Signature = await accessControlService.ReadSignature(item1.Stamp);
            }
            return dto;
        }


        public async Task<List<SignApprovalDTO>> ListAllSignuture(AuthenticatedUserDTO auth, long checklistId, long itemTemplateId)
        {
            try
            {


                var accessControlService = ObjectFactory.GetSingleton<IAccessControlService>();
                var res = await ItemChecklist.Repository.ListAllItensByChecklistIdAndIdTemplate(checklistId, itemTemplateId);
                List<SignApprovalDTO> lst = new List<SignApprovalDTO>();
                foreach (var item in res)
                {
                    var signature = await accessControlService.ReadSignature(item.Stamp);
                    signature.Comments = item.Comments;
                    lst.Add(signature);

                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
