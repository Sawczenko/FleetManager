using FleetManager.Modules.Itineraries.Domain.Checkpoints;
using FleetManager.Modules.Locations.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManager.Modules.Itineraries.Infrastructure.Domain;

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