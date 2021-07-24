using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Funda.Assignment.Application.Common.Interfaces;
using Funda.Assignment.Domain;
using Funda.Assignment.Domain.EstateAgents;
using MediatR;

namespace Funda.Assignment.Application.UseCases.EstateAgents.GetMostPropertiesForSaleByLocation
{
    public class EstateAgentsWithMostPropertiesQueryHandler :
        IRequestHandler<EstateAgentsWithMostPropertiesQuery, IQueryResult>
    {
        private const int DefaultLimit = 10;
        
        private readonly ISearchProperties _fundaPartnerApi;
        
        public EstateAgentsWithMostPropertiesQueryHandler(ISearchProperties fundaPartnerApi)
        {
            _fundaPartnerApi = fundaPartnerApi;
        }
        
        public async Task<IQueryResult> Handle(
            EstateAgentsWithMostPropertiesQuery request,
            CancellationToken cancellationToken)
        {
            var result = (await _fundaPartnerApi.SearchAsync(
                    location: request.Location.Value,
                    includeGarden: request.IncludeGarden,
                    cancellationToken: cancellationToken))
                .Where(p => !p.IsSold())
                .GroupBy(p => (p.EstateAgent.Id, p.EstateAgent.Name), (ea, p) => EstateAgent.New(ea.Id, ea.Name, p))
                .OrderByDescending(x => x.Properties.Count())
                .Take(DefaultLimit)
                .ToList();

            return new EstateAgentsWithMostPropertiesQueryResult(result);
        }
    }
}