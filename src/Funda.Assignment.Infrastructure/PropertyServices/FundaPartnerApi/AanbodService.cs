using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Funda.Assignment.Domain;

namespace Funda.Assignment.Infrastructure.PropertyServices.FundaPartnerApi
{
    public class AanbodService : ISearchProperties
    {
        private readonly Uri _fundaPartnerApiUri;
        private readonly ITranslateProperty<AanbodServiceResponse> _translator;

        public AanbodService(
            Uri fundaPartnerApiUri,
            ITranslateProperty<AanbodServiceResponse> translator)
        {
            _fundaPartnerApiUri = fundaPartnerApiUri;
            _translator = translator;
        }

        public async Task<IList<Property>> SearchAsync(string type, string location, bool withGarden)
        {
            //"http://partnerapi.funda.nl/feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/amsterdam/tuin/&page=1&pagesize=25";

            var properties = await _fundaPartnerApiUri
                .SetQueryParams(new
                {
                    type = type,
                    zo = location,
                    page = 1,
                    pagesize = 25
                })
                .WithTimeout(TimeSpan.FromSeconds(3))
                .GetJsonAsync<IList<AanbodServiceResponse>>();

            return properties.Select(property => _translator.Translate(property)).ToList();
        }
    }
}