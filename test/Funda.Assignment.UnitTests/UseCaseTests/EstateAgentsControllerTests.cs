using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Funda.Assignment.Api.UseCases.V1.EstateAgents.GetMostPropertiesForSaleByLocation;
using Funda.Assignment.Application.UseCases.EstateAgents.GetMostPropertiesForSaleByLocation;
using Funda.Assignment.Domain.EstateAgents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Funda.Assignment.UnitTests.UseCaseTests
{
    public class EstateAgentsControllerTests
    {
        [Fact]
        public async Task Should_Return_OK_With_Estate_Agents_With_Most_Properties_For_Sale_By_Location()
        {
            const string expectedLocation = "amsterdam";
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(x => x.Send(It.IsAny<EstateAgentsWithMostPropertiesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new EstateAgentsWithMostPropertiesQueryResult(new List<EstateAgent>()));
            var sut = new EstateAgentsController(mediatorMock.Object);

            var actualResult =
                await sut.GetEstateAgentsWithMostPropertiesForSaleByLocation(expectedLocation, includeGarden: false);

            actualResult.Should()
                .BeOfType<OkObjectResult>()
                .Subject.Value.Should()
                .BeOfType<List<GetMostPropertiesByLocationResponse>>();
        }
    }
}