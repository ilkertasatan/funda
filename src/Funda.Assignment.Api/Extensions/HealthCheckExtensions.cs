using Funda.Assignment.Api.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Funda.Assignment.Api.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddApiHealthChecks(this IServiceCollection services)
        {
            services
                .AddHealthChecks()
                .AddCheck<LivenessHealthCheck>("Liveness", HealthStatus.Unhealthy)
                .AddCheck<FundaPartnerApiHealthCheck>("FundaPartnerApi", HealthStatus.Unhealthy);

            return services;
        }
    }
}