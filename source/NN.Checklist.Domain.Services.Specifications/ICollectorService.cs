using TDCore.Core;
using TDCore.Data.Paging;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Request;
using NN.Checklist.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.Services.Specifications
{

    [ObjectMap("CollectorService", true)]
    public interface ICollectorService
    {
        public void CollectAlarms();
        public void CollectEvents();
        public void CollectExtremeValues();
    }
}
