using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FleetManager.Domain.Vehicles.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.Domain.Vehicles.Configurations
{
    internal class InspectionEntityConfiguration : IEntityTypeConfiguration<Inspection>
    {
        public void Configure(EntityTypeBuilder<Inspection> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Date)
                .IsRequired();

            builder.Property(i => i.Cost)
                .IsRequired();

            builder.HasOne<Vehicle>()
                .WithMany(v => v.Inspections)
                .HasConstraintName("FK_Inspection_Vehicle")
                .HasForeignKey(i => i.VehicleId);
        }
    }
}
