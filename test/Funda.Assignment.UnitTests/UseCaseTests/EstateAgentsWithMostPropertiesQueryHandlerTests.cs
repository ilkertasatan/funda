using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Funda.Assignment.Application.UseCases.EstateAgents.GetMostPropertiesForSaleByLocation;
using Funda.Assignment.Domain;
using Funda.Assignment.Domain.EstateAgents;
using Funda.Assignment.Domain.ValueObjects;
using Moq;
using Xunit;

namespace Funda.Assignment.UnitTests.UseCaseTests
{
    public class EstateAgentsWithMostPropertiesQueryHandlerTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Should_Handle_Query_And_Return_Estate_Agents_With_Most_Properties_For_Sale_By_Location(bool includeGarden)
        {
            const string expectedLocation = "amsterdam";
            var expectedEstateAgent = EstateAgent.New(new EstateAgentId(111), new Name("Estate-Agent-001"));
            var expectedProperties = new List<Property>
            {
                Property.New(
                    new PropertyId(111),
                    rented: false,
                    sold: false,
                    rentedOrSold: false,
                    new Price(300_000),
                    new Location(expectedLocation),
                    expectedEstateAgent)
            };
            var fundaPartnerApiMock = new Mock<ISearchProperties>();
            fundaPartnerApiMock
                .Setup(x => x.SearchAsync(It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedProperties);
            var sut = new EstateAgentsWithMostPropertiesQueryHandler(fundaPartnerApiMock.Object);

            var actualResult =
                await sut.Handle(
                    new EstateAgentsWithMostPropertiesQuery(expectedLocation, includeGarden),
                    CancellationToken.None);

            actualResult.Should()
                .BeOfType<EstateAgentsWithMostPropertiesQueryResult>()
                .Which.EstateAgents
                .Should().HaveCount(1);
        }
    }
}