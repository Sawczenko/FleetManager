namespace FleetManager.Domain.Aggregates.Vehicles;

public interface IVehicleRepository
{
    public Task<Vehicle?> GetById(Guid id, CancellationToken cancellationToken);

    public Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);
}