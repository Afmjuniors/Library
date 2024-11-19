using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Response
{
    public class CountryDTO
    {
        public System.Int32 CountryId { get; internal set; }
        public System.String Name { get; set; }
        public System.Int32 PrefixNumber { get; set; }
    }
}
