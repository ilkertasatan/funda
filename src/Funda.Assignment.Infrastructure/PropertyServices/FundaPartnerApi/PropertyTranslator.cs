using Funda.Assignment.Domain;
using Funda.Assignment.Domain.EstateAgents;
using Funda.Assignment.Domain.ValueObjects;

namespace Funda.Assignment.Infrastructure.PropertyServices.FundaPartnerApi
{
    public class PropertyTranslator : ITranslateProperty<AanbodServiceResponse.ObjectResponse>
    {
        public Property Translate(AanbodServiceResponse.ObjectResponse response)
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