namespace Funda.Assignment.Domain.ValueObjects
{
    public record EstateAgentName(string Name)
    {
        public override string ToString()
        {
            return Name;
        }
    }
}