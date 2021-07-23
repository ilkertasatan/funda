using Funda.Assignment.Domain;
using Funda.Assignment.Domain.EstateAgents;
using Funda.Assignment.Domain.ValueObjects;

namespace Funda.Assignment.Infrastructure.PropertyServices.FundaPartnerApi
{
    public class PropertyTranslator : ITranslateProperty<AanbodServiceResponse>
    {
        public Property Translate(AanbodServiceResponse response)
        {
            return Property.New(
                new PropertyId(response.GlobalId),
                response.IsVerhuurd,
                response.IsVerkocht,
                response.IsVerkochtOfVerhuurd,
                new PropertyPrice(response.KoopprijsTot),
                new PropertyLocation(response.Adres, response.Postcode),
                EstateAgent.New(
                    new EstateAgentId(response.MakelaarId),
                    new EstateAgentName(response.MakelaarNaam)));
        }
    }
}