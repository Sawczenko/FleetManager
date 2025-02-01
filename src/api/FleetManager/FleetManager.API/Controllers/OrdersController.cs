using FleetManager.Application.Orders.CreateOrder;
using FleetManager.Domain.SeedWork.Results;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace FleetManager.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Result>> CreateOrderAsync([FromBody] CreateOrderCommandRequest request, CancellationToken cancellationToken)
    {
        Result result = await _mediator.Send(new CreateOrderCommand(request), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}