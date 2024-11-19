using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Request.Parameter
{
    public class DomainParameterDTO
    {
        public string DomainAddress { get; set; }
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }
    }
}
