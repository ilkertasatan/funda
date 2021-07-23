using Funda.Assignment.Application.Common.Interfaces;
using Funda.Assignment.Domain.ValueObjects;
using MediatR;

namespace Funda.Assignment.Application.UseCases.EstateAgents.GetMostPropertiesForSaleByLocation
{
    public class EstateAgentsWithMostPropertiesQuery : IRequest<IQueryResult>
    {
        public EstateAgentsWithMostPropertiesQuery(string location, bool includeGarden)
        {
            Location = new Location(location);
            IncludeGarden = includeGarden;
        }
        
        public Location Location { get; }
        public bool IncludeGarden { get; }
    }
}