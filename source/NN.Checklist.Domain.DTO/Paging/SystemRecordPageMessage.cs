using TDCore.Data.Paging;
using NN.Checklist.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Paging
{
    public class SystemRecordPageMessage : PageMessage
    {

        #region Constructors
        public SystemRecordPageMessage()
        {

        }

        public SystemRecordPageMessage(int page, int pageSize)
        {
            this.ActualPage = page;
            this.PageSize = pageSize;
        }

        #endregion

        #region Attributes

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EnumSystemFunctionality? Functionality { get; set; }
        public long? UserId { get; set; }
        public string Keyword { get; set; }

        #endregion
    }
}
