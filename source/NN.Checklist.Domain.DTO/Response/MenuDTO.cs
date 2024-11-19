using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Response
{
    public class MenuDTO
    {
        public string Self { get; set; }
        public List<ItemMenuDTO> Items { get; set; }
    }
}
