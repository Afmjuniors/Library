using TDCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Domain;

namespace Library.Domain.Repositories.Specifications.Parameters
{
    [ObjectMap("MailParameterRepository", true)]
    public interface IMailParameterRepository<TEntity> : ITextFileRepositoryBase<TEntity> where TEntity : class
    {
    }
}
