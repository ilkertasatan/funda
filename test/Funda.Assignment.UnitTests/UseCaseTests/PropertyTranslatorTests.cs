using FluentAssertions;
using Funda.Assignment.Domain;
using Funda.Assignment.Infrastructure.PropertyServices.FundaPartnerApi;
using Xunit;

namespace Funda.Assignment.UnitTests.UseCaseTests
{
    public class PropertyTranslatorTests
    {
        [Fact]
        public void Should_Translate_Object_Response_To_Property()
        {
            var expectedObjectResponse = new AanbodServiceResponse.ObjectResponse
            {
                GlobalId = 111,
                Adres = "adres",
                MakelaarId = 222,
                MakelaarNaam = "makelaar-naam",
                KoopprijsTot = 300_000
            };
            var sut = new PropertyTranslator();

            var actualResult = sut.Translate(expectedObjectResponse);

            var actualProperty = actualResult.Should()
                .NotBeNull().And.Subject.Should()
                .BeOfType<Property>().Subject;

            actualProperty.Id.Value().Should().Be(expectedObjectResponse.GlobalId);
            actualProperty.Location.Value.Should().Be(expectedObjectResponse.Adres);
            actualProperty.Price.Value.Should().Be(expectedObjectResponse.KoopprijsTot);
            actualProperty.Sold.Should().BeFalse();
            actualProperty.Rented.Should().BeFalse();
            actualProperty.RentedOrSold.Should().BeFalse();
            actualProperty.EstateAgent.Id.Value().Should().Be(expectedObjectResponse.MakelaarId);
            actualProperty.EstateAgent.Name.Value.Should().Be(expectedObjectResponse.MakelaarNaam);
        }
    }
}