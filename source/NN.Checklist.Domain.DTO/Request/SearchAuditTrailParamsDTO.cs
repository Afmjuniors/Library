using NN.Checklist.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Request
{
    public class SearchParamsAuditTrailDTO
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Keyword { get; set; }
        public int? UserId { get; set; }
        public int? FunctionalityId { get; set; }
        
        public EnumSystemFunctionality? Functionality 
        { 
            get
            {
                if (FunctionalityId.HasValue)
                {
                    return (EnumSystemFunctionality)FunctionalityId.Value;
                }

                return null;
            }
        }        
    }
}
