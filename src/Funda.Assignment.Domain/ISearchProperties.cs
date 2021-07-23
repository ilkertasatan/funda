using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funda.Assignment.Domain
{
    public interface ISearchProperties
    {
        Task<IList<Property>> SearchAsync(string type, string location, bool withGarden);
    }
}