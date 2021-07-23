using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Funda.Assignment.Application.Common.Interfaces;
using Funda.Assignment.Domain;
using Funda.Assignment.Domain.EstateAgents;
using MediatR;

namespace Funda.Assignment.Application.UseCases.EstateAgents.GetPropertiesByLocation
{
    public class GetPropertiesByLocationQueryHandler : IRequestHandler<GetPropertiesByLocationQuery, IQueryResult>
    {
        private const int DefaultPage = 1;
        private const int DefaultPageSize = 25;
        private const int DefaultLimit = 10;
        
        private readonly ISearchProperties _fundaPartnerApi;
        
        public GetPropertiesByLocationQueryHandler(ISearchProperties fundaPartnerApi)
        {
            _fundaPartnerApi = fundaPartnerApi;
        }
        
        public async Task<IQueryResult> Handle(
            GetPropertiesByLocationQuery request,
            CancellationToken cancellationToken)
        {
            var result = (await _fundaPartnerApi.SearchAsync(
                    type: SearchType.Purchase,
                    location: request.Location.Value,
                    withGarden: false,
                    page: DefaultPage,
                    pageSize: DefaultPageSize,
                    cancellationToken))
                .Where(p => !p.IsSold())
                .GroupBy(p => (p.EstateAgent.Id, p.EstateAgent.Name), (ea, p) => EstateAgent.New(ea.Id, ea.Name, p))
                .OrderByDescending(x => x.Properties.Count())
                .Take(DefaultLimit)
                .ToList();

            return new GetPropertiesByLocationSuccessResult(result);
        }
    }
}