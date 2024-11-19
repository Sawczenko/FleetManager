namespace FleetManager.Domain.Aggregates.Vehicles;

public interface IVehicleRepository
{
    public Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    public Task<Vehicle?> GetByVehicleDetailsAsync(VehicleDetails vehicleData, CancellationToken cancellationToken);

    public Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);

    public Task<List<Vehicle>> GetVehiclesAsync(CancellationToken cancellationToken);
}