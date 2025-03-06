using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FleetManager.Modules.Locations.Domain;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Locations.Infrastructure
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
