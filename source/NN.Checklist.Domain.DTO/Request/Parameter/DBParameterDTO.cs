using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Request.Parameter
{
    public class DBParameterDTO
    {
        public string ConnectionStringSqlServer { get; set; }
        public string SqlServerSchema { get; set; }
    }
}
