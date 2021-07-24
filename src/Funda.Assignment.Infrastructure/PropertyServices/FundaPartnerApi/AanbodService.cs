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
        private const string DefaultSearchType = "koop"; //purchase
        private const int DefaultPageSize = 50;
        
        private readonly Uri _fundaPartnerApiUri;
        private readonly ITranslateProperty<AanbodServiceResponse.ObjectResponse> _translator;

        public AanbodService(
            Uri fundaPartnerApiUri,
            ITranslateProperty<AanbodServiceResponse.ObjectResponse> translator)
        {
            _fundaPartnerApiUri = fundaPartnerApiUri;
            _translator = translator;
        }

        public async Task<IEnumerable<Property>> SearchAsync(
            string location,
            bool includeGarden,
            CancellationToken cancellationToken)
        {
            var properties = await Policy
                .Handle<FlurlHttpException>(NeedsToBeRetried)
                .WaitAndRetryAsync(MaxRetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    AanbodServiceResponse response;
                    var objects = new List<AanbodServiceResponse.ObjectResponse>();

                    var pageIndex = 0;
                    do
                    {
                       response = await _fundaPartnerApiUri
                            .SetQueryParams(new
                            {
                                type = DefaultSearchType,
                                zo = includeGarden switch
                                {
                                    true => $"/{location}/tuin/",
                                    false => $"/{location}/"
                                },
                                page = pageIndex += 1,
                                pagesize = DefaultPageSize
                            })
                            .WithTimeout(TimeSpan.FromSeconds(3))
                            .GetJsonAsync<AanbodServiceResponse>(cancellationToken);

                       objects.AddRange(response.Objects);

                    } while (response.Objects is {Count: > 0});

                    return objects;
                });

            return properties.Select(property => _translator.Translate(property));
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