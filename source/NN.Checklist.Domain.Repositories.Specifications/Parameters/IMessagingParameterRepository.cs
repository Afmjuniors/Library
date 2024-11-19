using TDCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Domain;

namespace NN.Checklist.Domain.Repositories.Specifications.Parameters
{
    [ObjectMap("MessagingParameterRepository", true)]
    public interface IMessagingParameterRepository<TEntity> : ITextFileRepositoryBase<TEntity> where TEntity : class
    {
    }
}
