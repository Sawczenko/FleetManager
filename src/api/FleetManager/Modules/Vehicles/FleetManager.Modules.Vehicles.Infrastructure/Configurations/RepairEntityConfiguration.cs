using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FleetManager.Modules.Vehicles.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Vehicles.Infrastructure.Configurations
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
