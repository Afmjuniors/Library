using TDCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Domain;

namespace Library.Domain.Repositories.Specifications.Parameters
{
    [ObjectMap("DBParameterRepository", true)]
    public interface IDBParameterRepository<TEntity> : ITextFileRepositoryBase<TEntity> where TEntity : class
    {
    }
}
