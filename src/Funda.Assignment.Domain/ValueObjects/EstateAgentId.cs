namespace Funda.Assignment.Domain.ValueObjects
{
    public record EstateAgentId(int Id)
    {
        public int Value()
        {
            return Id;
        }
    }
}