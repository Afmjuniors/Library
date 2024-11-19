using TDCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Response
{
    public class AreaUpdatedDTO
    {
        public AreaDTO Data { get; set; }
        public String Comments { get; set; }
    }
}
