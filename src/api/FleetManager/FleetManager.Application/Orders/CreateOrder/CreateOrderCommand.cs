using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.Orders;
using MediatR;

namespace FleetManager.Application.Orders.CreateOrder;

public record CreateOrderCommand(CreateOrderCommandRequest CreateOrderRequest) : IRequest<Result>
{
    
}

internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
{
    private readonly OrderService _orderService;

    public CreateOrderCommandHandler(OrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        CreateOrderCommandRequest createOrderRequest = request.CreateOrderRequest;

        return await _orderService.CreateNewOrderAsync(
            Guid.Parse(createOrderRequest.ContractorId),
            Guid.Parse(createOrderRequest.OriginId),
            Guid.Parse(createOrderRequest.DestinationId),
            createOrderRequest.PickupDate,
            createOrderRequest.DeliveryDate,
            cancellationToken);
    }
}
