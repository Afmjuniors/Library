using Library.Domain.Entities.Parameters;
using Library.Domain.Repositories.Specifications.Parameters;
using TDCore.Data.TextFile;

namespace Library.Domain.Repositories.TextFile
{

    public class PolicyParameterRepository : RepositoryBase<PolicyParameter>, IPolicyParameterRepository<PolicyParameter>
    {
    }
}
