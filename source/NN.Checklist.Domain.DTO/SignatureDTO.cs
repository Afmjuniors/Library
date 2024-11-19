using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO
{
    public class SignatureDTO
    {
        public string username { get; set; }
        public string password { get; set; }
        public DateTime validationDate { get; set; }
        public bool result { get; set; }
        public string cryptData { get; set; }
    }
}
