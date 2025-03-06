using FleetManager.Modules.Vehicles.Infrastructure.Configurations;
using FleetManager.Modules.Vehicles.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Vehicles.Infrastructure;

public class VehiclesContext : DbContext
{
    public DbSet<Vehicle> Vehicles { get; set; } 
    public DbSet<Inspection> Inspections { get; set; }
    public DbSet<Repair> Repairs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("vehicles");
        ApplyConfigurations(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RepairEntityConfiguration());
        modelBuilder.ApplyConfiguration(new InspectionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleEntityConfiguration());
    }
}