using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FleetManager.Modules.Vehicles.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Vehicles.Infrastructure.Configurations
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
                .HasForeignKey(i => i.VehicleId);
        }
    }
}
