using FleetManager.Modules.Vehicles.Domain.Models;

namespace FleetManager.Modules.Vehicles.Domain;

public interface IVehicleRepository
{
    public Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    public Task<Vehicle?> GetByVehicleDetailsAsync(VehicleDetails vehicleData, CancellationToken cancellationToken);

    public Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);
}