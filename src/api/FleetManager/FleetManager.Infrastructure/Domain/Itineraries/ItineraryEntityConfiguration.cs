using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FleetManager.Infrastructure.Authentication;
using FleetManager.Domain.Vehicles.Models;
using FleetManager.Domain.Itinerary;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Routes;

namespace FleetManager.Infrastructure.Domain.Itineraries
{
    internal class ItineraryEntityConfiguration : IEntityTypeConfiguration<Itinerary>
    {
        public void Configure(EntityTypeBuilder<Itinerary> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ScheduledStartDate)
                .IsRequired();

            builder.Property(x => x.RouteId)
                .IsRequired();

            builder.Property(x => x.VehicleId)
                .IsRequired();

            builder.Property(x => x.DriverId)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired()
                .HasColumnType("INT");
        }
    }
}
