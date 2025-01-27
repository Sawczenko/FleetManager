
using FleetManager.Domain.Itinerary;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Routes;
using FleetManager.Domain.Locations;
using FleetManager.Domain.Vehicles.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManager.Infrastructure.Domain.Routes
{
    internal class RouteEntityConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(x => x.StartLocationId)
                .IsRequired();

            builder.Property(x => x.EndLocationId)
                .IsRequired();

            builder.HasOne<Location>()
                .WithMany()
                .HasForeignKey(x => x.StartLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Location>()
                .WithMany()
                .HasForeignKey(x => x.EndLocationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
