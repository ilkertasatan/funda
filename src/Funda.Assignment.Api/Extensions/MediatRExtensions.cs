using Funda.Assignment.Application.UseCases.EstateAgents.GetPropertiesByLocation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Funda.Assignment.Api.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetPropertiesByLocationQuery).Assembly);
            
            return services;
        }
    }
}