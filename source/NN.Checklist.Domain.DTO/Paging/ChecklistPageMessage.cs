using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Data.Paging;

namespace NN.Checklist.Domain.DTO.Paging
{
    public class ChecklistPageMessage : PageMessage
    {
        public ChecklistPageMessage()
        {

        }

        public ChecklistPageMessage(int page, int pageSize)
        {
            this.ActualPage = page;
            this.PageSize = pageSize;
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        public bool? isBatchClosed { get; set; }
        public long? Batch { get; set; }
        public string ChecklistTemplateDescription { get; set; }
        public string VersionChecklistTemplateDescription { get; set; }


    }
}
