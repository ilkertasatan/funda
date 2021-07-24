using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Funda.Assignment.Domain
{
    public interface ISearchProperties
    {
        Task<IEnumerable<Property>> SearchAsync(
            string location,
            bool includeGarden,
            CancellationToken cancellationToken);
    }

    public interface ICheckPropertyServiceIsHealthy
    {
        Task Ping();
    }
}