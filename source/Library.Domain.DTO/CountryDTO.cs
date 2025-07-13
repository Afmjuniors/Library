using Library.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Domain;

namespace Library.Domain.DTO
{
    public class CountryDTO
    {

        public System.Int32 CountryId { get; internal set; }

        public System.String Name { get; set; }

        public System.Int32 PrefixNumber { get; set; }

    }
}
