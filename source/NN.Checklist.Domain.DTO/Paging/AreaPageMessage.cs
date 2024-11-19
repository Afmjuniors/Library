using TDCore.Data.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Paging
{
    public class AreaPageMessage : PageMessage
    {
        public AreaPageMessage()
        {

        }

        public AreaPageMessage(int page, int pageSize)
        {
            this.ActualPage = page;
            this.PageSize = pageSize;
        }

        public Int64 AreaId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Int64 ProcessId { get; set; }
    }
}
