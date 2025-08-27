using Library.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Domain;

namespace Library.Domain.DTO
{
    public class OrganizationDTO
    {

        public System.Int64 OrganizationId { get; internal set; }

        public System.DateTime? CreatedAt { get; set; }

        public System.String Name { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public System.DateTime? UpdatedAt { get; set; }

        public System.String Description { get; set; }

        public System.String Image { get; set; }

        public List<UserDTO> Members { get; set; }

        public List<OrganizationRulesDTO> OrganizationRules { get; set; }


    }
}
