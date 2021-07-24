using System;
using System.Threading;
using System.Threading.Tasks;
using Funda.Assignment.Domain;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Funda.Assignment.Api.HealthChecks
{
    public class FundaPartnerApiHealthCheck : IHealthCheck
    {
        private readonly ICheckPropertyServiceIsHealthy _fundaPartnerApi;

        public FundaPartnerApiHealthCheck(ICheckPropertyServiceIsHealthy fundaPartnerApi)
        {
            _fundaPartnerApi = fundaPartnerApi;
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = new())
        {
            try
            {
                await _fundaPartnerApi.Ping();
                
                return HealthCheckResult.Healthy();
            }
            catch (Exception)
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}