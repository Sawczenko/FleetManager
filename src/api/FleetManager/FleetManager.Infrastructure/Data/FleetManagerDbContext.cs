using FleetManager.Infrastructure.Domain.Vehicles.Configurations;
using FleetManager.Infrastructure.Domain.VehicleUsages;
using FleetManager.Infrastructure.Domain.Locations;
using FleetManager.Infrastructure.Domain.Routes;
using FleetManager.Infrastructure.Domain.Users;
using FleetManager.Domain.Vehicles.Models;
using FleetManager.Domain.VehicleUsages;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Routes;
using FleetManager.Domain.User;

namespace FleetManager.Infrastructure.Data
{
    public class FleetManagerDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
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
            modelBuilder.ApplyConfiguration(new LocationEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RouteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RouteStopEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RepairEntityConfiguration());
            modelBuilder.ApplyConfiguration(new InspectionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FuelExpenseEntityConfiguration());
        }
    }
}
