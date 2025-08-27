using Library.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Domain;

namespace Library.Domain.DTO
{
    public class RegulationDTO
    {

        public System.Int64 RegulationId { get; internal set; }

        public System.DateTime? UpdatedAt { get; set; }

        public System.Int64 OrganizationId { get; set; }
        public EnumRules RuleId { get; set; }
        public string Value { get; set; }

    }
}
