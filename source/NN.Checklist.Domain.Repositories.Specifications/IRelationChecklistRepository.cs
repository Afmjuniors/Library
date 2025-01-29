using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Domain;

namespace NN.Checklist.Domain.Repositories.Specifications
{
    [ObjectMap("RelationChecklistRepository", true)]
    public interface IRelationChecklistRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {
    }
}

