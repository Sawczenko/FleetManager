using FleetManager.Modules.Itineraries.Infrastructure.Domain;
using FleetManager.Modules.Itineraries.Domain;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Itineraries.Infrastructure;

public class ItinerariesContext : DbContext
{
    public DbSet<Itinerary> Itineraries { get; set; } 
    
    public ItinerariesContext(DbContextOptions<ItinerariesContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("itineraries");
        ApplyConfigurations(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CheckpointEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ItineraryEntityConfiguration());
    }
}