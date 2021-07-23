using System.Collections.Generic;
using Funda.Assignment.Application.Common.Interfaces;
using Funda.Assignment.Domain.EstateAgents;

namespace Funda.Assignment.Application.UseCases.EstateAgents.GetMostPropertiesForSaleByLocation
{
    public class EstateAgentsWithMostPropertiesQueryResult : IQueryResult
    {
        public EstateAgentsWithMostPropertiesQueryResult(IEnumerable<EstateAgent> estateAgents)
        {
            EstateAgents = estateAgents;
        }

        public IEnumerable<EstateAgent> EstateAgents { get; }
    }
}