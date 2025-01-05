using FleetManager.Application.Routes.GetRoutes;
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
    }
}
