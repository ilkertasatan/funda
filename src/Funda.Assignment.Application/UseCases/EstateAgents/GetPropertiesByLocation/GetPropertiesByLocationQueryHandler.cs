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
            var properties = await _fundaPartnerApi.SearchAsync("koop", request.Location.Address, false);

            return new GetPropertiesByLocationSuccessResult(properties);
        }
    }
}