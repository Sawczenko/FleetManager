﻿
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
        }
    }
}
