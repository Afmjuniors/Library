using TDCore.Data.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.DTO.Request;

namespace Library.Domain.DTO.Paging
{
    public class BookPageMessage : PageMessage
    {
        public BookPageMessage()
        {

        }


        public BookPageMessage(int page, int pageSize)
        {
            this.ActualPage = page;
            this.PageSize = pageSize;
        }

        public SearchBookParamsDTO? Filter { get; set; }


    }
}
