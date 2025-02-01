﻿using FleetManager.Domain.Contractors;
using FleetManager.Domain.Locations;
using FleetManager.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManager.Infrastructure.Domain.Orders
{
    internal class OrdersEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(x => x.OriginId)
                .IsRequired();

            builder.Property(x => x.DestinationId)
                .IsRequired();

            builder.Property(x => x.PickupDate)
                .IsRequired();

            builder.Property(x => x.DeliveryDate)
                .IsRequired();

            builder.Property(x => x.ContractorId)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired()
                .HasColumnType("INT");

            builder.HasOne<Contractor>()
                .WithMany()
                .HasForeignKey(x => x.ContractorId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne<Location>()
                .WithMany()
                .HasForeignKey(x => x.OriginId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Location>()
                .WithMany()
                .HasForeignKey(x => x.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
