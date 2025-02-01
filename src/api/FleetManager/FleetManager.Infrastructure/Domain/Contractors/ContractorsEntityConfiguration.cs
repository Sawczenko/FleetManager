using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FleetManager.Domain.Contractors;
using FleetManager.Domain.Locations;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.Domain.Contractors
{
    internal class ContractorsEntityConfiguration : IEntityTypeConfiguration<Contractor>
    {
        public void Configure(EntityTypeBuilder<Contractor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.HeadquartersId)
                .IsRequired();

            builder.HasOne<Location>()
                .WithMany()
                .HasForeignKey(x => x.HeadquartersId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
