using FleetManager.Infrastructure.Domain.Vehicles.Configurations;
using FleetManager.Infrastructure.Domain.VehicleUsages;
using FleetManager.Infrastructure.Domain.Locations;
using FleetManager.Domain.Aggregates.VehicleUsages;
using FleetManager.Infrastructure.Domain.Routes;
using FleetManager.Infrastructure.Domain.Users;
using FleetManager.Domain.Aggregates.Vehicles;
using FleetManager.Domain.Aggregates.Routes;
using FleetManager.Domain.Aggregates.User;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.Data
{
    internal class FleetManagerDbContext : DbContext
    {
        public required DbSet<User> Users { get; set; } // Użytkownicy domenowi
        public required DbSet<VehicleUsage> VehicleUsages { get; set; } // Użycia pojazdów
        public required DbSet<FuelExpense> FuelExpenses { get; set; } // Wydatki na paliwo
        public required DbSet<Route> Routes { get; set; } // Trasy
        public required DbSet<RouteStop> RouteStops { get; set; }
        public required DbSet<Inspection> Inspections { get; set; } // Przeglądy
        public required DbSet<Repair> Repairs { get; set; } // Naprawy

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
