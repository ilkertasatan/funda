using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Funda.Assignment.Domain;
using Microsoft.Extensions.Options;
using Polly;

namespace Funda.Assignment.Infrastructure.PropertyServices.FundaPartnerApi
{
    public class AanbodService : 
        ISearchProperties,
        ICheckPropertyServiceIsHealthy
    {
        private const int MaxRetryCount = 5;
        private const string DefaultSearchType = "koop"; //purchase
        private const int DefaultPageSize = 100;
        
        private readonly Uri _fundaPartnerApiUri;
        private readonly ITranslateProperty<AanbodServiceResponse.ObjectResponse> _translator;

        public AanbodService(
            IOptions<FundaPartnerApiSettings> options,
            ITranslateProperty<AanbodServiceResponse.ObjectResponse> translator)
        {
            _fundaPartnerApiUri = new Uri($"{options.Value.ApiUrl}/{options.Value.ApiKey}");
            _translator = translator;
        }

        public async Task<IEnumerable<Property>> SearchAsync(
            string location,
            bool includeGarden,
            CancellationToken cancellationToken)
        {
            var response = await CallAanbodService(
                location,
                includeGarden,
                pageIndex: 0,
                pageSize: DefaultPageSize,
                cancellationToken);
            var pageCount = response.Paging.AantalPaginas;
                    
            var allTasks = new List<Task<IEnumerable<AanbodServiceResponse.ObjectResponse>>>();
            for (var pageIndex = 1; pageIndex < pageCount; pageIndex++)
                allTasks.Add(AcquireObjectsAsync(
                    location,
                    includeGarden,
                    pageIndex,
                    cancellationToken));

            var allResults = await Task.WhenAll(allTasks);

            var objects = new List<AanbodServiceResponse.ObjectResponse>();
            objects.AddRange(allResults.SelectMany(task => task));

            return objects.Select(property => _translator.Translate(property));
        }

        public async Task PingAsync()
        {
            await CallAanbodService(
                DefaultSearchType,
                includeGarden: false,
                pageIndex: 0,
                pageSize: 1,
                CancellationToken.None);
        }

        private async Task<IEnumerable<AanbodServiceResponse.ObjectResponse>> AcquireObjectsAsync(
            string location,
            bool includeGarden,
            int pageIndex,
            CancellationToken cancellationToken)
        {
            var response = await CallAanbodService(
                location,
                includeGarden,
                pageIndex,
                DefaultPageSize,
                cancellationToken);
            return response.Objects;
        }

        private async Task<AanbodServiceResponse> CallAanbodService(
            string location,
            bool includeGarden,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken)
        {
            return await Policy
                .Handle<FlurlHttpException>(NeedsToBeRetried)
                .WaitAndRetryAsync(MaxRetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    return await _fundaPartnerApiUri
                        .SetQueryParams(new
                        {
                            type = DefaultSearchType,
                            zo = includeGarden switch
                            {
                                true => $"/{location}/tuin/",
                                false => $"/{location}/"
                            },
                            page = pageIndex,
                            pagesize = pageSize
                        })
                        .WithTimeout(TimeSpan.FromSeconds(3))
                        .GetJsonAsync<AanbodServiceResponse>(cancellationToken)
                        .ConfigureAwait(false);
                });
        }

        private static bool NeedsToBeRetried(FlurlHttpException ex)
        {
            return (HttpStatusCode) ex.Call.Response.StatusCode switch
            {
                HttpStatusCode.Unauthorized => true,
                HttpStatusCode.RequestTimeout => true,
                HttpStatusCode.InternalServerError => true,
                HttpStatusCode.BadGateway => true,
                HttpStatusCode.GatewayTimeout => true,
                HttpStatusCode.TooManyRequests => true,
                _ => false
            };
        }
    }
}