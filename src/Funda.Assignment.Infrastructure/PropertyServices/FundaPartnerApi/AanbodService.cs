using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Funda.Assignment.Domain;

namespace Funda.Assignment.Infrastructure.PropertyServices.FundaPartnerApi
{
    public class AanbodService : ISearchProperties
    {
        private readonly Uri _fundaPartnerApiUri;
        private readonly ITranslateProperty<AanbodServiceResponse.Object> _translator;

        public AanbodService(
            Uri fundaPartnerApiUri,
            ITranslateProperty<AanbodServiceResponse.Object> translator)
        {
            _fundaPartnerApiUri = fundaPartnerApiUri;
            _translator = translator;
        }

        public async Task<IList<Property>> SearchAsync(
            SearchType type,
            string location,
            bool withGarden,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            //"http://partnerapi.funda.nl/feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/amsterdam/tuin/&page=1&pagesize=25";

            var property = await _fundaPartnerApiUri
                .SetQueryParams(new
                {
                    type = type switch
                    {
                        SearchType.Purchase => "koop",
                        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                    },
                    zo = $"/{location}",
                    page = page,
                    pagesize = pageSize
                })
                .WithTimeout(TimeSpan.FromSeconds(3))
                .GetJsonAsync<AanbodServiceResponse>(cancellationToken);

            return property.Objects.Select(@object => _translator.Translate(@object)).ToList();
        }
    }
}