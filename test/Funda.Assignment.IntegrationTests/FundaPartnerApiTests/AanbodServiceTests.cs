using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Funda.Assignment.Domain;
using Funda.Assignment.Infrastructure.PropertyServices.FundaPartnerApi;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Funda.Assignment.IntegrationTests.FundaPartnerApiTests
{
    public class AanbodServiceTests
    {
        private const string FundaPartnerApiUrl = "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/";
        private const string ApiKey = "ac1b0b1572524640a0ecc54de453ea9f";

        private readonly AanbodService _sut;
        private readonly CancellationToken _cancellationToken;

        public AanbodServiceTests()
        {
            var optionsMock = new Mock<IOptions<FundaPartnerApiSettings>>();
            optionsMock
                .SetupGet(x => x.Value)
                .Returns(new FundaPartnerApiSettings
                {
                    ApiUrl = FundaPartnerApiUrl,
                    ApiKey = ApiKey
                });
         
            _sut = new AanbodService(optionsMock.Object, new PropertyTranslator());

            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(10));

            _cancellationToken = cancellationTokenSource.Token;
        }
        
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Should_Return_Properties(bool includeGarden)
        {
            const string expectedLocation = "amsterdam";
            
            var actualResult = (await _sut.SearchAsync(
                expectedLocation,
                includeGarden,
                _cancellationToken)).ToList();

            actualResult.Should()
                .BeOfType<List<Property>>()
                .Subject.Should()
                .NotBeEmpty().And.HaveCountGreaterThan(0);
        }
        
        [Fact]
        public async Task Should_Return_Empty_Properties_Given_Invalid_Location()
        {
            const string expectedLocation = "invalid-location";
            
            var actualResult = (await _sut.SearchAsync(
                expectedLocation,
                includeGarden: false,
                _cancellationToken)).ToList();

            actualResult.Should()
                .BeOfType<List<Property>>()
                .Subject.Should()
                .BeEmpty().And.HaveCount(0);
        }
    }
}