using FleetManager.Modules.Orders.Domain;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Orders.Infrastructure.Domain;

internal class OrderRepository : IOrderRepository
{
    private readonly DbSet<Order> _orders;

    public OrderRepository(OrdersContext ordersContext)
    {
        _orders = ordersContext.Set<Order>();
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        await _orders.AddAsync(order, cancellationToken);
    }
}