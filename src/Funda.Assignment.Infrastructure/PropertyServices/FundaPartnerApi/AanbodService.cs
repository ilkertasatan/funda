using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Funda.Assignment.Domain;
using Polly;

namespace Funda.Assignment.Infrastructure.PropertyServices.FundaPartnerApi
{
    public class AanbodService : ISearchProperties
    {
        private const int MaxRetryCount = 3;
        
        private readonly Uri _fundaPartnerApiUri;
        private readonly ITranslateProperty<AanbodServiceResponse.ObjectResponse> _translator;

        public AanbodService(
            Uri fundaPartnerApiUri,
            ITranslateProperty<AanbodServiceResponse.ObjectResponse> translator)
        {
            _fundaPartnerApiUri = fundaPartnerApiUri;
            _translator = translator;
        }

        public async Task<IList<Property>> SearchAsync(
            SearchType type,
            string location,
            bool includeGarden,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var properties = await Policy
                .Handle<FlurlHttpException>(NeedsToBeRetried)
                .WaitAndRetryAsync(MaxRetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    AanbodServiceResponse response;
                    var objects = new List<AanbodServiceResponse.ObjectResponse>();

                    do
                    {
                       response = await _fundaPartnerApiUri
                            .SetQueryParams(new
                            {
                                type = type switch
                                {
                                    SearchType.Purchase => "koop",
                                    _ => "koop"
                                },
                                zo = includeGarden switch
                                {
                                    true => $"/{location}/tuin/",
                                    false => $"/{location}/"
                                },
                                page = page += 1,
                                pagesize = pageSize
                            })
                            .WithTimeout(TimeSpan.FromSeconds(3))
                            .GetJsonAsync<AanbodServiceResponse>(cancellationToken);

                       objects.AddRange(response.Objects);

                    } while (response.Objects is {Count: > 0});

                    return objects;
                });

            return properties.Select(@object => _translator.Translate(@object)).ToList();
        }

        private static bool NeedsToBeRetried(FlurlHttpException ex)
        {
            switch ((HttpStatusCode) ex.Call.Response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.RequestTimeout:
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.GatewayTimeout:
                case HttpStatusCode.TooManyRequests:
                    return true;
                default:
                    return false;
            }
        }
    }
}