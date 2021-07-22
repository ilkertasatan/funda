using Microsoft.Extensions.DependencyInjection;

namespace Funda.Assignment.Api.Extensions
{
    public static class ApiVersioningExtensions
    {
        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(
                options => { options.ReportApiVersions = true; });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            return services;
        } 
    }
}