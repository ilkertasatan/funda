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
        }
        
        public Name Name { get; }
        
        public static EstateAgent New(
            EstateAgentId estateAgentId,
            Name name)
        {
            return new(estateAgentId, name);
        }
    }
}