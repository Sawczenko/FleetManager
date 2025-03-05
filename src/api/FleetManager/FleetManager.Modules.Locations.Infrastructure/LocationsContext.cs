using FleetManager.Modules.Locations.Domain;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Locations.Infrastructure;

public class LocationsContext : DbContext
{
    public DbSet<Location> Locations { get; set; }

    public LocationsContext(DbContextOptions<LocationsContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("locations");
        ApplyConfigurations(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LocationEntityConfiguration());
    }
}