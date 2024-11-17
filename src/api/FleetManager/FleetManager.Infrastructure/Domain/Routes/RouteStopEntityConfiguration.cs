using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Aggregates.Locations;
using FleetManager.Domain.Aggregates.Routes;

namespace FleetManager.Infrastructure.Domain.Routes
{
    internal class RouteStopEntityConfiguration : IEntityTypeConfiguration<RouteStop>
    {
        public void Configure(EntityTypeBuilder<RouteStop> builder)
        {
            builder.HasKey(rs => rs.Id);

            // Właściwości wymagane
            builder.Property(rs => rs.SequenceNumber)
                .IsRequired();

            builder.Property(rs => rs.ArrivalTime)
                .IsRequired();

            builder.Property(rs => rs.DepartureTime)
                .IsRequired();

            // Relacja z Route (Many-to-One)
            builder.HasOne<Route>()
                .WithMany(r => r.RouteStops)
                .HasForeignKey(rs => rs.RouteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacja z Location (Many-to-One)
            builder.HasOne<Location>()
                .WithMany()
                .HasForeignKey(rs => rs.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
