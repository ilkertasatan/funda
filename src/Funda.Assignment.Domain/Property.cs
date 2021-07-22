using Funda.Assignment.Domain.EstateAgents;
using Funda.Assignment.Domain.ValueObjects;

namespace Funda.Assignment.Domain
{
    public class Property : Entity<PropertyId>
    {
        private Property() { }

        private Property(
            PropertyId propertyId,
            bool isRented,
            bool isSold,
            bool isRentedOrSold,
            PropertyPrice price,
            PropertyLocation location,
            EstateAgent estateAgent)
        {
            Id = propertyId;
            IsRented = isRented;
            IsSold = isSold;
            IsRentedOrSold = isRentedOrSold;
            Location = location;
            Price = price;
            EstateAgent = estateAgent;
        }
        
        public bool IsRented { get; }
        public bool IsSold { get; }
        public bool IsRentedOrSold { get; }
        public PropertyLocation Location { get; }
        public PropertyPrice Price { get; }
        public EstateAgent EstateAgent { get; }
        

        public static Property New(
            PropertyId propertyId,
            bool isRented,
            bool isSold,
            bool isRentedOrSold,
            PropertyPrice price,
            PropertyLocation location,
            EstateAgent estateAgent)
        {
            return new(
                propertyId,
                isRented,
                isSold,
                isRentedOrSold,
                price,
                location,
                estateAgent);
        }
    }
}