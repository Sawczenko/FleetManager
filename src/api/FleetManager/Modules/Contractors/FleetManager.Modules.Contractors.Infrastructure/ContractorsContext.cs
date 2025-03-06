using FleetManager.Modules.Contractors.Domain;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Contractors.Infrastructure;

public class ContractorsContext : DbContext
{
    public DbSet<Contractor> Contractors { get; set; }

    public ContractorsContext(DbContextOptions<ContractorsContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("contractors");
        ApplyConfigurations(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContractorsEntityConfiguration());
    }

}