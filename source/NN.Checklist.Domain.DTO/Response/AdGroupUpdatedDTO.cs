using NN.Checklist.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Response
{
    public class AdGroupUpdatedDTO
    {
        public AdGroupDTO Data { get; set; }
        public System.String Comments { get; set; }
    }
}
