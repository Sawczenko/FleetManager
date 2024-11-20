﻿using FleetManager.Application.Vehicles.Queries.GetVehicles;
using FleetManager.Domain.Vehicles.Models;
using FleetManager.Domain.Vehicles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetVehiclesQuery()));
        }
    }
}