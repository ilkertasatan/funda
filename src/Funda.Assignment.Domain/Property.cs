using Funda.Assignment.Domain.EstateAgents;
using Funda.Assignment.Domain.ValueObjects;

namespace Funda.Assignment.Domain
{
    public class Property : Entity<PropertyId>
    {
        public static readonly Property Empty = new Property();
        private Property() { }

        private Property(
            PropertyId propertyId,
            bool rented,
            bool sold,
            bool rentedOrSold,
            Price price,
            Location location,
            EstateAgent estateAgent)
        {
            Id = propertyId;
            Rented = rented;
            Sold = sold;
            RentedOrSold = rentedOrSold;
            Location = location;
            Price = price;
            EstateAgent = estateAgent;
        }
        
        public bool Rented { get; }
        public bool Sold { get; }
        public bool RentedOrSold { get; }
        public Location Location { get; }
        public Price Price { get; }
        public EstateAgent EstateAgent { get; }
        
        public static Property New(
            PropertyId propertyId,
            bool rented,
            bool sold,
            bool rentedOrSold,
            Price price,
            Location location,
            EstateAgent estateAgent)
        {
            return new(
                propertyId,
                rented,
                sold,
                rentedOrSold,
                price,
                location,
                estateAgent);
        }

        public bool IsSold() => Sold;
        public bool IsRented() => Rented;
        public bool IsRentedOrSold() => RentedOrSold;
    }
}