using FleetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Vehicles;
using FleetManager.Domain.Vehicles.Models;

namespace FleetManager.Infrastructure.Domain.Vehicles
{
    internal sealed class VehicleRepository : IVehicleRepository
    {
        private readonly DbSet<Vehicle> _vehicles;

        public VehicleRepository(FleetManagerDbContext fleetManagerDbContext)
        {
            _vehicles = fleetManagerDbContext.Set<Vehicle>();
        }

        public async Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _vehicles
                .Include(x => x.Repairs)
                .Include(x => x.Inspections)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Vehicle?> GetByVehicleDetailsAsync(VehicleDetails vehicleDetails, CancellationToken cancellationToken)
        {
            return await _vehicles
                .Where(x => x.VehicleDetails.Vin == vehicleDetails.Vin
                || x.VehicleDetails.LicensePlate == vehicleDetails.LicensePlate)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            _vehicles.AddAsync(vehicle, cancellationToken);

            return Task.CompletedTask;
        }
    }
}
