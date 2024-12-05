using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.Identity
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
