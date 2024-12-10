using FleetManager.Infrastructure.Domain.Vehicles.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FleetManager.Infrastructure.Domain.VehicleUsages;
using FleetManager.Infrastructure.Domain.Locations;
using FleetManager.Infrastructure.Domain.Routes;
using FleetManager.Domain.Vehicles.Models;
using FleetManager.Domain.VehicleUsages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FleetManager.Domain.Routes;
using FleetManager.Infrastructure.Authentication;

namespace FleetManager.Infrastructure.Data
{
    public class FleetManagerDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleUsage> VehicleUsages { get; set; }
        public DbSet<FuelExpense> FuelExpenses { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteStop> RouteStops { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<Repair> Repairs { get; set; }

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
            modelBuilder.ApplyConfiguration(new RouteStopEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RepairEntityConfiguration());
            modelBuilder.ApplyConfiguration(new InspectionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FuelExpenseEntityConfiguration());
        }
    }
}
