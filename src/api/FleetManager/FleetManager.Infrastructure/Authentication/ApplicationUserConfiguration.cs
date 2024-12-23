﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManager.Infrastructure.Authentication
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(100).IsRequired();

            builder.HasMany(x => x.VehicleUsages)
                .WithOne()
                .HasForeignKey(y => y.UserId)
                .HasConstraintName("FK_ApplicationUser_VehicleUsage");
        }
    }
}
