namespace Funda.Assignment.Domain.ValueObjects
{
    public record Name(string Value)
    {
        public override string ToString()
        {
            return Value;
        }
    }
}