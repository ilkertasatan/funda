using Funda.Assignment.Domain.ValueObjects;

namespace Funda.Assignment.Domain.EstateAgents
{
    public class EstateAgent
    {
        private EstateAgent() { }

        private EstateAgent(
            EstateAgentId estateAgentId,
            EstateAgentName name)
        {
            EstateAgentId = estateAgentId;
            Name = name;
        }

        public EstateAgentId EstateAgentId { get; }
        public EstateAgentName Name { get; }


        public static EstateAgent New(
            EstateAgentId estateAgentId,
            EstateAgentName name)
        {
            return new(estateAgentId, name);
        }
    }
}