using System.Collections.Generic;
using Funda.Assignment.Application.Common.Interfaces;
using Funda.Assignment.Domain;
using Funda.Assignment.Domain.EstateAgents;

namespace Funda.Assignment.Application.UseCases.EstateAgents.GetPropertiesByLocation
{
    public class GetPropertiesByLocationSuccessResult : IQueryResult
    {
        public GetPropertiesByLocationSuccessResult(IList<EstateAgent> estateAgents)
        {
            EstateAgents = estateAgents;
        }

        public IList<EstateAgent> EstateAgents { get; }
    }
}