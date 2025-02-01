﻿namespace FleetManager.Domain.Orders;

public interface IOrderRepository
{
    public Task AddAsync(Order order, CancellationToken cancellationToken);
}