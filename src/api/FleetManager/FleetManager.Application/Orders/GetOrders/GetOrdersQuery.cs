using FleetManager.Domain.Contractors;
using FleetManager.Domain.Locations;
using FleetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Orders;
using MediatR;

namespace FleetManager.Application.Orders.GetOrders;

public record GetOrdersQuery(OrdersFilterDto OrdersFilter) : IRequest<List<OrderDto>>
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
        var query = _dbContext.Database
            .SqlQuery<OrdersQuery>(@$"
                SELECT orders.Id, 
                contractors.Id as ContractorId, contractors.Name as ContractorName,
                originLocations.Id as OriginLocationId, originLocations.Name as OriginLocationName, 
                destinationLocations.Id as DestinationLocationId, destinationLocations.Name as DestinationLocationName,
                orders.PickupDate, orders.DeliveryDate, orders.Status
                FROM Orders orders
                JOIN Locations originLocations ON originLocations.Id = orders.OriginId
                JOIN Locations destinationLocations ON destinationLocations.Id = orders.DestinationId
                JOIN Contractors contractors ON contractors.Id = orders.ContractorId
            ");

        OrdersFilterDto ordersFilter = request.OrdersFilter;

        if (ordersFilter.ContractorId is not null && ordersFilter.ContractorId != Guid.Empty)
        {
            query = query.Where(x => x.ContractorId == ordersFilter.ContractorId);
        }

        if (ordersFilter.OriginLocationId is not null && ordersFilter.OriginLocationId != Guid.Empty)
        {
            query = query.Where(x => x.OriginLocationId == ordersFilter.OriginLocationId);
        }

        if (ordersFilter.DestinationLocationId is not null && ordersFilter.DestinationLocationId != Guid.Empty)
        {
            query = query.Where(x => x.DestinationLocationId == ordersFilter.DestinationLocationId);
        }

        if (ordersFilter.PickupDateFrom != default)
        {
            query = query.Where(x => x.PickupDate > ordersFilter.PickupDateFrom);
        }

        if (ordersFilter.PickupDateTo != default)
        {
            query = query.Where(x => x.PickupDate < ordersFilter.PickupDateTo);
        }

        if (ordersFilter.DeliveryDateFrom != default)
        {
            query = query.Where(x => x.DeliveryDate > ordersFilter.DeliveryDateFrom);
        }

        if (ordersFilter.DeliveryDateTo != default)
        {
            query = query.Where(x => x.DeliveryDate < ordersFilter.DeliveryDateTo);
        }

        return query
            .Select(ordersQuery => new OrderDto(
                ordersQuery.Id,
                new ContractorInfo(ordersQuery.ContractorId, ordersQuery.ContractorName),
                new LocationInfo(ordersQuery.OriginLocationId, ordersQuery.OriginLocationName),
                new LocationInfo(ordersQuery.DestinationLocationId, ordersQuery.DestinationLocationName),
                ordersQuery.PickupDate,
                ordersQuery.DeliveryDate,
                ((OrderStatus)ordersQuery.Status).ToString()
                ))
            .ToListAsync(cancellationToken);
    }
}

internal record OrdersQuery(
    Guid Id,
    Guid ContractorId,
    string ContractorName,
    Guid OriginLocationId,
    string OriginLocationName,
    Guid DestinationLocationId,
    string DestinationLocationName,
    DateTime PickupDate,
    DateTime DeliveryDate,
    int Status
)
{ }