using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Funda.Assignment.Application.UseCases.EstateAgents.GetPropertiesByLocation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Funda.Assignment.Api.UseCases.V1.EstateAgents.GetPropertiesByLocation
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
        [HttpGet("{location}/properties", Name = "Location")]
        public async Task<IActionResult> GetPropertiesByLocation(
            [FromRoute] [Required] string location,
            [FromQuery] int offset = 1,
            [FromQuery] int limit = 10)
        {
            var result = await _mediator.Send(new GetPropertiesByLocationQuery(location, offset, limit));
            return Output.For(result);
        }
    }
}