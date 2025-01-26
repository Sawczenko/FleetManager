using FleetManager.Application.Routes.CreateRoute;
using FleetManager.Application.Routes.GetRoutePlannerForm;
using FleetManager.Application.Routes.GetRoutePlannerForm.Dto;
using FleetManager.Application.Routes.GetRoutes;
using FleetManager.Domain.SeedWork.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoutesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoutesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<RouteDto>>> GetRoutesAsync([FromQuery] RoutesFilterDto routesFilter, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetRoutesQuery(routesFilter), cancellationToken));
        }

        [HttpGet("route-planner")]
        public async Task<ActionResult<RoutePlannerFormDto>> GetRoutePlannerFormAsync(
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetRoutePlannerFormQuery(), cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<Result>> AddRouteAsync([FromBody] CreateRouteRequest addRouteRequest, CancellationToken cancellationToken)
        {
            Result result = await _mediator.Send(new CreateRouteCommand(addRouteRequest), cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
