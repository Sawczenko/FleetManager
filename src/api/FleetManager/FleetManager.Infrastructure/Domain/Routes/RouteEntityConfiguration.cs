using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Routes;
using FleetManager.Domain.Locations;

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
                .HasConstraintName("Route_StartLocation")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Location>()
                .WithMany()
                .HasForeignKey("EndLocationId")
                .HasConstraintName("Route_EndLocation")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
