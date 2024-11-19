using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO
{
    public class SignApprovalDTO
    {
        public string Initials { get; set; }
        public DateTime DthSign { get; set; }
        public bool Result { get; set; }
    }
}
