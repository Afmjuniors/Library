using TDCore.Data.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.DTO.Paging
{
    public class UsersPageMessage : PageMessage
    {
        public UsersPageMessage()
        {

        }

        public UsersPageMessage(int page, int pageSize)
        {
            this.ActualPage = page;
            this.PageSize = pageSize;
        }

        public String Initials { get; set; }
    }
}
