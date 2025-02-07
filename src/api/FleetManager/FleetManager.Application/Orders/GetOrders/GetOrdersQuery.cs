using FleetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Orders;
using MediatR;

namespace FleetManager.Application.Orders.GetOrders;

public record GetOrdersQuery : IRequest<List<OrderDto>>
{
    
}

internal class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderDto>>
{
    private readonly FleetManagerDbContext _dbContext;

    public GetOrdersQueryHandler(FleetManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        return _dbContext.Database
            .SqlQuery<OrdersQuery>(@$"
                SELECT orders.Id, contractors.Name as ContractorName,
                originLocations.Name as OriginLocation, destinationLocations.Name as DestinationLocation,
                orders.PickupDate, orders.DeliveryDate, orders.Status
                FROM Orders orders
                JOIN Locations originLocations ON originLocations.Id = orders.OriginId
                JOIN Locations destinationLocations ON destinationLocations.Id = orders.DestinationId
                JOIN Contractors contractors ON contractors.Id = orders.ContractorId
            ")
            .Select(ordersQuery => new OrderDto(
                ordersQuery.Id,
                ordersQuery.ContractorName,
                ordersQuery.OriginLocation,
                ordersQuery.DestinationLocation,
                ordersQuery.PickupDate,
                ordersQuery.DeliveryDate,
                ((OrderStatus)ordersQuery.Status).ToString()
                ))
            .ToListAsync(cancellationToken);
    }
}

internal record OrdersQuery(
    Guid Id,
    string ContractorName,
    string OriginLocation,
    string DestinationLocation,
    DateTime PickupDate,
    DateTime DeliveryDate,
    int Status
)
{ }