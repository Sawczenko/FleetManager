using FleetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Aggregates.Vehicles;

namespace FleetManager.Infrastructure.Domain.Vehicles
{
    internal sealed class VehicleRepository : IVehicleRepository
    {
        private readonly DbSet<Vehicle> _vehicles;

        public VehicleRepository(FleetManagerDbContext fleetManagerDbContext)
        {
            _vehicles = fleetManagerDbContext.Set<Vehicle>();
        }

        public async Task<Vehicle?> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _vehicles
                .Include(x => x.Repairs)
                .Include(x => x.Inspections)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            _vehicles.AddAsync(vehicle, cancellationToken);

            return Task.CompletedTask;
        }
    }
}
