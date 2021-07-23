using Funda.Assignment.Domain.ValueObjects;

namespace Funda.Assignment.Domain.EstateAgents
{
    public class EstateAgent : Entity<EstateAgentId>
    {
        private EstateAgent() { }

        private EstateAgent(
            EstateAgentId estateAgentId,
            EstateAgentName name)
        {
            Id = estateAgentId;
            Name = name;
        }
        
        public EstateAgentName Name { get; }
        
        public static EstateAgent New(
            EstateAgentId estateAgentId,
            EstateAgentName name)
        {
            return new(estateAgentId, name);
        }
    }
}