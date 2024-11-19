using TDCore.Data.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Paging
{
    public class AdGroupPageMessage : PageMessage
    {
        public AdGroupPageMessage()
        {

        }

        public AdGroupPageMessage(int page, int pageSize)
        {
            this.ActualPage = page;
            this.PageSize = pageSize;
        }

        public String Name { get; set; }
    }
}
