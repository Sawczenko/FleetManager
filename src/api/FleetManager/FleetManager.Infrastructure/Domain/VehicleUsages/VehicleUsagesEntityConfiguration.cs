using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Aggregates.VehicleUsages;
using FleetManager.Domain.Aggregates.Vehicles;
using FleetManager.Domain.Aggregates.User;

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
