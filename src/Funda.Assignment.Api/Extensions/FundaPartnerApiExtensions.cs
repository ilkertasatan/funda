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
            services.AddScoped<ISearchProperties, AanbodService>();
            services.AddScoped<ICheckPropertyServiceIsHealthy, AanbodService>();

            services.Configure<FundaPartnerApiSettings>(options => 
                configuration.GetSection("PropertyServices:FundaPartnerApi").Bind(options));
            
            return services;
        }
    }
}