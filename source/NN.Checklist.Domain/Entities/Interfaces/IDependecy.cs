using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Domain;

namespace NN.Checklist.Domain.Entities.Interfaces
{
    public interface IDependecy
    {

        public System.Int64? DependentBlockVersionChecklistTemplateId { get; set; }

        public System.Int64? DependentItemVersionChecklistTemplateId { get; set; }

    }
}
