using FleetManager.Infrastructure.Data;
using FleetManager.Domain.SeedWork;

namespace FleetManager.Infrastructure.Domain
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly FleetManagerDbContext _fleetManagerDbContext;

        public UnitOfWork(FleetManagerDbContext fleetManagerDbContext)
        {
            _fleetManagerDbContext = fleetManagerDbContext;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _fleetManagerDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
