using FleetManager.Domain.Routes;
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

            builder
                .Property(vu => vu.UserId)
                .IsRequired();

            builder.HasOne<Vehicle>()
                .WithMany()
                .HasForeignKey(vu => vu.VehicleId)
                .HasConstraintName("FK_VehicleUsage_Vehicle")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Route>()
                .WithMany()
                .HasForeignKey(vu => vu.RouteId)
                .HasConstraintName("FK_VehicleUsage_Route")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
