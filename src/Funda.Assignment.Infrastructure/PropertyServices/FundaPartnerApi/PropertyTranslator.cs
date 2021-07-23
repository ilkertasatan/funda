using Funda.Assignment.Domain;
using Funda.Assignment.Domain.EstateAgents;
using Funda.Assignment.Domain.ValueObjects;

namespace Funda.Assignment.Infrastructure.PropertyServices.FundaPartnerApi
{
    public class PropertyTranslator : ITranslateProperty<AanbodServiceResponse.Object>
    {
        public Property Translate(AanbodServiceResponse.Object response)
        {
            return Property.New(
                new PropertyId(response.GlobalId),
                response.IsVerhuurd,
                response.IsVerkocht,
                response.IsVerkochtOfVerhuurd,
                new Price(response.KoopprijsTot),
                new Location(response.Adres),
                EstateAgent.New(
                    new EstateAgentId(response.MakelaarId),
                    new Name(response.MakelaarNaam)));
        }
    }
}