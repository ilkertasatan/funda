using Funda.Assignment.Application.Common.Interfaces;
using Funda.Assignment.Domain.ValueObjects;
using MediatR;

namespace Funda.Assignment.Application.UseCases.EstateAgents.GetMostPropertiesForSaleByLocation
{
    public class EstateAgentsWithMostPropertiesQuery : IRequest<IQueryResult>
    {
        public EstateAgentsWithMostPropertiesQuery(string location)
        {
            Location = new Location(location);
        }
        
        public Location Location { get; }
    }
}