using FleetManager.Modules.Vehicles.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManager.Modules.Vehicles.Infrastructure.Configurations
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

            builder.Property(x => x.Status)
                .IsRequired()
                .HasColumnType("INT");
        }
    }
}
