using System.Collections.Generic;
using Funda.Assignment.Application.Common.Interfaces;
using Funda.Assignment.Domain;

namespace Funda.Assignment.Application.UseCases.EstateAgents.GetPropertiesByLocation
{
    public class GetPropertiesByLocationSuccessResult : IQueryResult
    {
        public GetPropertiesByLocationSuccessResult(IList<Property> properties)
        {
            Properties = properties;
        }

        public IList<Property> Properties { get; }
    }
}