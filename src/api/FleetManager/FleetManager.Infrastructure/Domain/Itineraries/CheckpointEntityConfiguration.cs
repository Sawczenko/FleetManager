using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FleetManager.Domain.Itineraries;
using FleetManager.Domain.Itineraries.Checkpoints;
using FleetManager.Domain.Locations;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.Domain.Itineraries;

public class CheckpointEntityConfiguration : IEntityTypeConfiguration<Checkpoint>
{
    public void Configure(EntityTypeBuilder<Checkpoint> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne<Location>()
            .WithMany()
            .HasForeignKey(x => x.LocationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasColumnType("INT");

        builder.Property(x => x.Type)
            .IsRequired()
            .HasColumnType("INT");
    }
}