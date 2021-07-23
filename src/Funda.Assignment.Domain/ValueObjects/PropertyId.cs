namespace Funda.Assignment.Domain.ValueObjects
{
    public record PropertyId(int Id)
    {
        public int Value()
        {
            return Id;
        }
    }
}