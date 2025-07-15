using Library.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.DTO.Request
{
    public class SearchBookParamsDTO
    {

        public string KeyWord { get; set; }
        public long? UserId { get; set; }
        public long? OwnerId { get; set; }
        public long? OrganizationId { get; set; }
        public string UserName { get; set; }
        public string Author { get; set; }
        public EnumGenre? Genre { get; set; }
        public EnumBookStatus? BookStatus{ get; set; }
        public EnumLoanStatus? LoanStatus { get; set; }



    }
}
