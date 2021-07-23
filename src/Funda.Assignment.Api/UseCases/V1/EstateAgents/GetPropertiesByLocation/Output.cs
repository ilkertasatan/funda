using System.Collections.Generic;
using System.Linq;
using Funda.Assignment.Application.Common.Interfaces;
using Funda.Assignment.Application.UseCases.EstateAgents.GetPropertiesByLocation;
using Funda.Assignment.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Funda.Assignment.Api.UseCases.V1.EstateAgents.GetPropertiesByLocation
{
    public static class Output
    {
        public static IActionResult For(IQueryResult output) =>
            output switch
            {
                GetPropertiesByLocationSuccessResult result => Ok(result.Properties),
                _ => InternalServerError()
            };

        private static IActionResult Ok(IEnumerable<Property> properties)
        {
            return new OkObjectResult(properties
                .Select(property => new GetPropertiesByLocationResponse
                {
                    EstateAgentId = property.EstateAgent.Id.Value(),
                    EstateAgentName = property.EstateAgent.Name.ToString()
                })
                .ToList());
        }

        private static IActionResult InternalServerError()
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}