using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FleetManager.Domain.Vehicles.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManager.Infrastructure.Domain.Vehicles.Configurations
{
    internal class RepairEntityConfiguration : IEntityTypeConfiguration<Repair>
    {
        public void Configure(EntityTypeBuilder<Repair> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Date)
                .IsRequired();

            builder.Property(r => r.Cost)
                .IsRequired();

            builder.HasOne<Vehicle>()
                .WithMany(v => v.Repairs)
                .HasForeignKey(r => r.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
