using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Aggregates.Locations;

namespace FleetManager.Infrastructure.Domain.Locations
{
    internal class LocationEntityConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Latitude)
                .IsRequired();

            builder.Property(l => l.Longitude)
                .IsRequired();
        }
    }
}
