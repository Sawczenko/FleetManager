using FleetManager.Modules.Orders.Infrastructure.Domain.Contractors;
using FleetManager.Modules.Orders.Infrastructure.Domain.Orders;
using FleetManager.Modules.Orders.Domain.Contractors;
using FleetManager.Modules.Orders.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Orders.Infrastructure;

public class OrdersContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Contractor> Contractors { get; set; }

    public OrdersContext(DbContextOptions<OrdersContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("orders");
        ApplyConfigurations(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrdersEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ContractorsEntityConfiguration());
    }
    
}