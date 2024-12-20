using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Routes;
using FleetManager.Domain.Locations;
using FleetManager.Domain.Vehicles.Models;
using FleetManager.Domain.VehicleUsages;

namespace FleetManager.Infrastructure.Domain.Routes
{
    internal class RouteEntityConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Status)
                .IsRequired();

            builder.Property(r => r.ScheduledStartTime)
                .IsRequired();

            builder.Property(r => r.ActualEndTime)
                .IsRequired(false);

            builder.HasOne<Location>()
                .WithMany()
                .HasForeignKey("StartLocationId")
                .HasConstraintName("FK_Route_StartLocation")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Location>()
                .WithMany()
                .HasForeignKey("EndLocationId")
                .HasConstraintName("FK_Route_EndLocation")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany<VehicleUsage>()
                .WithOne()
                .HasForeignKey(y => y.RouteId)
                .HasConstraintName("FK_Route_VehicleUsage");
        }
    }
}
