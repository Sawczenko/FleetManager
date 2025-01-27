using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Routes;
using FleetManager.Domain.Itineraries;

namespace FleetManager.Infrastructure.Domain.Itineraries;

public class ItineraryRouteEntityConfiguration : IEntityTypeConfiguration<ItineraryRoute>
{
    public void Configure(EntityTypeBuilder<ItineraryRoute> builder)
    {
        builder.HasKey(ir => ir.Id);

        builder.HasOne<Route>()
            .WithMany()
            .HasForeignKey(ir => ir.RouteId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(ir => ir.Order)
            .IsRequired();
    }
}