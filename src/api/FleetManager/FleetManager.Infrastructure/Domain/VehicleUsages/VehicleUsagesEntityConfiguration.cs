using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FleetManager.Domain.Vehicles.Models;
using FleetManager.Domain.VehicleUsages;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.User;

namespace FleetManager.Infrastructure.Domain.VehicleUsages
{
    internal class VehicleUsagesEntityConfiguration : IEntityTypeConfiguration<VehicleUsage>
    {
        public void Configure(EntityTypeBuilder<VehicleUsage> builder)
        {
            builder.HasKey(vu => vu.Id);

            builder.Property(vu => vu.StartDate)
                .IsRequired();

            builder.Property(vu => vu.EndDate)
                .IsRequired(false);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId")
                .HasConstraintName("VehicleUsage_User")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Vehicle>()
                .WithMany()
                .HasForeignKey(vu => vu.VehicleId)
                .HasConstraintName("VehicleUsage_Vehicle")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
