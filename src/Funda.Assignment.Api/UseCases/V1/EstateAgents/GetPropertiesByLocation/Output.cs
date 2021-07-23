﻿using System.Collections.Generic;
using System.Linq;
using Funda.Assignment.Application.Common.Interfaces;
using Funda.Assignment.Application.UseCases.EstateAgents.GetPropertiesByLocation;
using Funda.Assignment.Domain.EstateAgents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Funda.Assignment.Api.UseCases.V1.EstateAgents.GetPropertiesByLocation
{
    public static class Output
    {
        public static IActionResult For(IQueryResult output) =>
            output switch
            {
                GetPropertiesByLocationSuccessResult result => Ok(result.EstateAgents),
                _ => InternalServerError()
            };

        private static IActionResult Ok(IEnumerable<EstateAgent> estateAgents)
        {
            return new OkObjectResult(estateAgents
                .Select(estateAgent => new GetPropertiesByLocationResponse
                {
                    EstateAgentId = estateAgent.Id.Value(),
                    EstateAgentName = estateAgent.Name.ToString(),
                    Properties = estateAgent.Properties.Select(p => new PropertyResponse
                    {
                        Id = p.Id.Value(),
                        Address = p.Location.Value,
                        RentedOrSold = p.IsRentedOrSold()
                    })
                })
                .ToList());
        }

        private static IActionResult InternalServerError()
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}