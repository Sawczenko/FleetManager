using FleetManager.Modules.Orders.Infrastructure.Domain;
using FleetManager.Modules.Orders.Domain;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Orders.Infrastructure;

public class OrdersContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("orders");
        ApplyConfigurations(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrdersEntityConfiguration());
    }
    
}