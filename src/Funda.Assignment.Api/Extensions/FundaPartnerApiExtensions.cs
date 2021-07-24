using System;
using Funda.Assignment.Domain;
using Funda.Assignment.Infrastructure.PropertyServices.FundaPartnerApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Funda.Assignment.Api.Extensions
{
    public static class FundaPartnerApiExtensions
    {
        public static IServiceCollection AddFundaPartnerApi(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ISearchProperties>(provider =>
            {
                var apiKey = configuration["PropertyServices:FundaPartnerApi:ApiKey"];
                var uri = new Uri(configuration["PropertyServices:FundaPartnerApi:ApiUrl"]);
                var translator = provider.GetRequiredService<ITranslateProperty<AanbodServiceResponse.ObjectResponse>>();

                return new AanbodService(new Uri(uri, apiKey), translator);
            });
            return services;
        }
    }
}