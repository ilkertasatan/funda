using Funda.Assignment.Domain;
using Funda.Assignment.Infrastructure.PropertyServices.FundaPartnerApi;
using Microsoft.Extensions.DependencyInjection;

namespace Funda.Assignment.Api.Extensions
{
    public static class UseCaseExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ITranslateProperty<AanbodServiceResponse.Object>, PropertyTranslator>();
            
             return services;
        }
    }
}