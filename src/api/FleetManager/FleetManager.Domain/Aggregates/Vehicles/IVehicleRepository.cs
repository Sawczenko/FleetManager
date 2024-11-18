namespace FleetManager.Domain.Aggregates.Vehicles;

public interface IVehicleRepository
{
    public Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    public Task<Vehicle?> GetByVehicleDataAsync(VehicleDetails vehicleData, CancellationToken cancellationToken);

    public Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);
}