using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Funda.Assignment.Domain
{
    public interface ISearchProperties
    {
        Task<IList<Property>> SearchAsync(
            SearchType type,
            string location,
            bool includeGarden, 
            int page,
            int pageSize,
            CancellationToken cancellationToken);
    }
}