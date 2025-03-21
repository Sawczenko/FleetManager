using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FleetManager.Modules.Orders.Domain.Contractors;
using FleetManager.Modules.Orders.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Orders.Infrastructure.Domain.Contractors
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
            
            builder.HasMany<Order>()
                .WithOne()
                .HasForeignKey(y => y.ContractorId)
                .HasConstraintName("FK_Contractor_Order")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
