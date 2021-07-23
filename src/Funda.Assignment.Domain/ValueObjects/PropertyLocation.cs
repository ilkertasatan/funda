namespace Funda.Assignment.Domain.ValueObjects
{
    public record PropertyLocation
    {
        public PropertyLocation(string address)
        {
            Address = address;
        }
        
        public PropertyLocation(string address, string postCode)
        {
            Address = address;
            PostCode = postCode;
        }
        
        public string Address { get; }
        public string PostCode { get; }
    }
}