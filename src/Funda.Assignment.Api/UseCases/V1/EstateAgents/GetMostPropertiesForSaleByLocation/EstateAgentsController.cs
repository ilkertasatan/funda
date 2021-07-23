using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Funda.Assignment.Application.UseCases.EstateAgents.GetMostPropertiesForSaleByLocation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Funda.Assignment.Api.UseCases.V1.EstateAgents.GetMostPropertiesForSaleByLocation
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("v{version:apiVersion}/estate-agents")]
    public class EstateAgentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EstateAgentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{location}/most-properties-for-sale")]
        public async Task<IActionResult> GetEstateAgentsWithMostPropertiesForSaleByLocation(
            [FromRoute] [Required] string location,
            [FromQuery] bool includeGarden)
        {
            return Output.For(await _mediator.Send(new EstateAgentsWithMostPropertiesQuery(location, includeGarden)));
        }
    }
}