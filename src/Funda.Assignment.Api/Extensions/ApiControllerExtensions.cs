using Microsoft.Extensions.DependencyInjection;

namespace Funda.Assignment.Api.Extensions
{
    public static class ApiControllerExtensions
    {
        public static IServiceCollection AddApiControllers(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = null);

            return services;
        }
    }
}