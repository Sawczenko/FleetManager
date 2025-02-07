using FleetManager.Domain.Orders;
using FleetManager.Domain.Vehicles.Models;
using FleetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.Domain.Orders;

internal class OrderRepository : IOrderRepository
{
    private readonly DbSet<Order> _orders;

    public OrderRepository(FleetManagerDbContext fleetManagerDbContext)
    {
        _orders = fleetManagerDbContext.Set<Order>();
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        await _orders.AddAsync(order, cancellationToken);
    }
}