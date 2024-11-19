using NN.Checklist.Domain.Common;
using NN.Checklist.Domain.DTO.Request.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Response
{
    public class DomainParameterUpdatedDTO
    {
        public DomainParameterDTO Data { get; set; }
        public System.String Comments { get; set; }
    }
}
