using FleetManager.Domain.Locations;
using FleetManager.Infrastructure.Domain.Vehicles.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FleetManager.Infrastructure.Domain.Locations;
using FleetManager.Infrastructure.Domain.Routes;
using FleetManager.Domain.Vehicles.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FleetManager.Domain.Routes;
using FleetManager.Infrastructure.Authentication;
using FleetManager.Infrastructure.Domain.Itineraries;
using FleetManager.Domain.Itineraries;

namespace FleetManager.Infrastructure.Data
{
    public class FleetManagerDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }
        public DbSet<ItineraryRoute> ItineraryRoutes { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Location> Locations { get; set; }

        public FleetManagerDbContext(DbContextOptions<FleetManagerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            ApplyConfigurations(modelBuilder);
        }

        private void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new LocationEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RouteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ItineraryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ItineraryRouteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RepairEntityConfiguration());
            modelBuilder.ApplyConfiguration(new InspectionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleEntityConfiguration());
        }
    }
}
