using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.VehicleUsages;
using FleetManager.Domain.Routes;

namespace FleetManager.Infrastructure.Domain.VehicleUsages
{
    internal class FuelExpenseEntityConfiguration : IEntityTypeConfiguration<FuelExpense>
    {
        public void Configure(EntityTypeBuilder<FuelExpense> builder)
        {
            builder.HasKey(fe => fe.Id);

            builder.Property(fe => fe.Liters)
                .IsRequired();

            builder.Property(fe => fe.FuelType)
                .IsRequired();

            builder.Property(fe => fe.VehicleUsageId)
                .IsRequired();

            builder.HasOne<Route>()
                .WithMany()
                .HasForeignKey(fe => fe.RouteId)
                .HasConstraintName("FuelExpense_Route")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<VehicleUsage>()
                .WithMany(fe => fe.FuelExpenses)
                .HasForeignKey(fe => fe.VehicleUsageId)
                .HasConstraintName("FuelExpense_VehicleUsage")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
