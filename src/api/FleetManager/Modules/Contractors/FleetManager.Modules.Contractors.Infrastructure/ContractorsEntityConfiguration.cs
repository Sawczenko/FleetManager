using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FleetManager.Modules.Contractors.Domain;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Contractors.Infrastructure
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
        }
    }
}
