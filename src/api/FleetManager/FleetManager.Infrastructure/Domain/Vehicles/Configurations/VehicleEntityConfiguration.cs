using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FleetManager.Domain.Aggregates.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.Domain.Vehicles.Configurations
{
    public class VehicleEntityConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(v => v.Id);

            builder.OwnsOne(v => v.VehicleDetails, vDetails =>
            {
                vDetails.Property(v => v.Vin)
                    .IsRequired()
                    .HasMaxLength(17)
                    .HasColumnName("VIN");
                vDetails.Property(v => v.LicensePlate)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("LicensePlate");
                vDetails.Property(v => v.Model)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Model");

                vDetails.HasIndex(v => v.Vin).IsUnique();
            });

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
