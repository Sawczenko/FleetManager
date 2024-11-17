using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FleetManager.Domain.Aggregates.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .HasConstraintName("Inspection_Vehicle")
                .HasForeignKey(i => i.VehicleId);
        }
    }
}
