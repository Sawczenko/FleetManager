using FleetManager.Modules.Itineraries.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManager.Modules.Itineraries.Infrastructure.Domain
{
    internal class ItineraryEntityConfiguration : IEntityTypeConfiguration<Itinerary>
    {
        public void Configure(EntityTypeBuilder<Itinerary> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StartDate)
                .IsRequired();

            builder.Property(x => x.EndDate)
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
