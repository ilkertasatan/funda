using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Funda.Assignment.Application.Common.Interfaces;
using Funda.Assignment.Domain;
using MediatR;

namespace Funda.Assignment.Application.UseCases.EstateAgents.GetPropertiesByLocation
{
    public class GetPropertiesByLocationQueryHandler : IRequestHandler<GetPropertiesByLocationQuery, IQueryResult>
    {
        private readonly ISearchProperties _fundaPartnerApi;

        public GetPropertiesByLocationQueryHandler(ISearchProperties fundaPartnerApi)
        {
            _fundaPartnerApi = fundaPartnerApi;
        }

        public async Task<IQueryResult> Handle(GetPropertiesByLocationQuery request, CancellationToken cancellationToken)
        {
            var properties = (await _fundaPartnerApi.SearchAsync("koop", request.Location.Value, false))
                .Where(p => !p.IsSold())
                .GroupBy(p => (p.EstateAgent.Id, p.EstateAgent.Name), (key, p) => new
                {
                    EstateAgentId = key.Id,
                    EstateAgentName = key.Name,
                    Properties = p.ToList()
                });

            foreach (var property in properties)
            {
                
            }

            return new GetPropertiesByLocationSuccessResult(new List<Property>());
        }
    }
}