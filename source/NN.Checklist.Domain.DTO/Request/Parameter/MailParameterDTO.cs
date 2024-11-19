using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Request.Parameter
{
    public class MailParameterDTO
    {
        public string SMTPServer { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPFromAddress { get; set; }
        public string SMTPPassword { get; set; }
        public bool SMTPEnabledSSL { get; set; }
    }
}
