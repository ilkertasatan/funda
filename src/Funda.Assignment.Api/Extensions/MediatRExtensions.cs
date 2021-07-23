using Funda.Assignment.Application.UseCases.EstateAgents.GetMostPropertiesForSaleByLocation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Funda.Assignment.Api.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(EstateAgentsWithMostPropertiesQuery).Assembly);
            
            return services;
        }
    }
}