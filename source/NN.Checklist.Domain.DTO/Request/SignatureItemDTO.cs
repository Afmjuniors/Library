using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Request
{
    public class SignatureItemDTO
    {
        public ItemChecklistDTO Data { get; set; }
        public string Comments { get; set; }
    }
}
