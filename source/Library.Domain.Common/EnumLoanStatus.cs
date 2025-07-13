using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Common
{
    public enum EnumLoanStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
        Received = 4,
        Returned = 5,
        Overdue = 6
    }
}
