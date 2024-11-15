using FleetManager.Domain.Locations;
using FleetManager.Domain.Routes;
using FleetManager.Domain.User;
using FleetManager.Domain.Vehicles;
using FleetManager.Domain.VehicleUsages;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.Data
{
    internal class FleetManagerDbContext : DbContext
    {
        public DbSet<User> DomainUsers { get; set; } // Użytkownicy domenowi
        public DbSet<VehicleUsage> VehicleUsages { get; set; } // Użycia pojazdów
        public DbSet<FuelExpense> FuelExpenses { get; set; } // Wydatki na paliwo
        public DbSet<Route> Routes { get; set; } // Trasy
        public DbSet<Inspection> Inspections { get; set; } // Przeglądy
        public DbSet<Repair> Repairs { get; set; } // Naprawy

        public FleetManagerDbContext(DbContextOptions<FleetManagerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureUser(modelBuilder);
            ConfigureVehicle(modelBuilder);
            ConfigureVehicleUsage(modelBuilder);
            ConfigureRoute(modelBuilder);
            ConfigureFuelExpense(modelBuilder);
            ConfigureInspection(modelBuilder);
            ConfigureRepair(modelBuilder);
            ConfigureLocation(modelBuilder);
        }

        private void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.FirstName)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(u => u.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(u => u.Email)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(u => u.Password)
                      .IsRequired();
            });
        }

        private void ConfigureVehicle(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(v => v.Id);

                entity.Property(v => v.Vin)
                      .IsRequired()
                      .HasMaxLength(17);

                entity.Property(v => v.LicensePlate)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(v => v.Model)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(v => v.LastInspectionDate)
                      .IsRequired();

                entity.HasOne(v => v.CurrentLocation)
                      .WithMany()
                      .HasForeignKey("CurrentLocationId")
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureVehicleUsage(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleUsage>(entity =>
            {
                entity.HasKey(vu => vu.Id);

                entity.Property(vu => vu.StartDate)
                      .IsRequired();

                entity.Property(vu => vu.EndDate)
                      .IsRequired(false);

                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Vehicle>()
                      .WithMany()
                      .HasForeignKey(vu => vu.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureRoute(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Route>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Status)
                      .IsRequired();

                entity.Property(r => r.ScheduledStartTime)
                      .IsRequired();

                entity.Property(r => r.ActualEndTime)
                      .IsRequired(false);

                entity.HasOne<Location>()
                      .WithMany()
                      .HasForeignKey("StartLocationId")
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Location>()
                      .WithMany()
                      .HasForeignKey("EndLocationId")
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureFuelExpense(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FuelExpense>(entity =>
            {
                entity.HasKey(fe => fe.Id);

                entity.Property(fe => fe.Liters)
                      .IsRequired();

                entity.Property(fe => fe.FuelType)
                      .IsRequired();

                entity.HasOne<Route>()
                      .WithMany()
                      .HasForeignKey(fe => fe.RouteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<VehicleUsage>()
                      .WithMany()
                      .HasForeignKey(fe => fe.VehicleUsageId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureInspection(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inspection>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.Property(i => i.Date)
                      .IsRequired();

                entity.Property(i => i.Cost)
                      .IsRequired();

                entity.HasOne<Vehicle>()
                      .WithMany()
                      .HasForeignKey(i => i.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureRepair(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Repair>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Date)
                      .IsRequired();

                entity.Property(r => r.Cost)
                      .IsRequired();

                entity.HasOne<Vehicle>()
                      .WithMany()
                      .HasForeignKey(r => r.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureLocation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(l => l.Id);

                entity.Property(l => l.Latitude)
                      .IsRequired();

                entity.Property(l => l.Longitude)
                      .IsRequired();
            });
        }
    }
}
