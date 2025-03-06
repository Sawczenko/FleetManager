namespace FleetManager.Modules.Orders.Domain;

public interface IOrderRepository
{
    public Task AddAsync(Order order, CancellationToken cancellationToken);
}