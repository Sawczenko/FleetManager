namespace FleetManager.Modules.Orders.Domain.Orders;

public interface IOrderRepository
{
    public Task AddAsync(Order order, CancellationToken cancellationToken);
}