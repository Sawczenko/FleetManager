using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Aggregates.Vehicles;

namespace FleetManager.Infrastructure.Domain.Vehicles.Configurations
{
    public class VehicleEntityConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Vin)
                    .IsRequired()
                    .HasMaxLength(17);

            builder.Property(v => v.LicensePlate)
                    .HasMaxLength(20)
                    .IsRequired();

            builder.Property(v => v.Model)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(v => v.LastInspectionDate)
                    .IsRequired();

            builder.HasOne(v => v.CurrentLocation)
                    .WithMany()
                    .HasForeignKey("CurrentLocationId")
                    .HasConstraintName("Vehicle_CurrentLocation")
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
