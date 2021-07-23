using System.ComponentModel;
using Funda.Assignment.Application.Common.Interfaces;
using Funda.Assignment.Domain.ValueObjects;
using MediatR;

namespace Funda.Assignment.Application.UseCases.EstateAgents.GetPropertiesByLocation
{
    public class GetPropertiesByLocationQuery : IRequest<IQueryResult>
    {
        public GetPropertiesByLocationQuery(string location, int offset, int limit)
        {
            Location = new PropertyLocation(location);
            Offset = offset;
            Limit = limit;
        }
        
        public PropertyLocation Location { get; }
        public int Offset { get; }
        public int Limit { get; }
    }
}