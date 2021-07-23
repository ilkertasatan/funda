using System.Collections.Generic;
using Funda.Assignment.Domain.ValueObjects;

namespace Funda.Assignment.Domain.EstateAgents
{
    public class EstateAgent : Entity<EstateAgentId>
    {
        private EstateAgent() { }

        private EstateAgent(
            EstateAgentId estateAgentId,
            Name name)
        {
            Id = estateAgentId;
            Name = name;
            Properties = new List<Property>();
        }
        
        private EstateAgent(
            EstateAgentId estateAgentId,
            Name name,
            IEnumerable<Property> properties)
        {
            Id = estateAgentId;
            Name = name;
            Properties = properties;
        }
        
        public Name Name { get; }
        public IEnumerable<Property> Properties { get; }

        public static EstateAgent New(
            EstateAgentId estateAgentId,
            Name name) => new(estateAgentId, name);
        
        public static EstateAgent New(
            EstateAgentId estateAgentId,
            Name name,
            IEnumerable<Property> properties) => new(estateAgentId, name, properties);
    }
}