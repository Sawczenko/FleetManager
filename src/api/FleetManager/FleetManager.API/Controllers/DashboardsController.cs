using FleetManager.Application.Dashboard.Models;
using FleetManager.Application.Dashboard.Queries.GetDashboard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DashboardsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<DashboardDto>> GetDashboardAsync(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetDashboardQuery(), cancellationToken));
    }
}