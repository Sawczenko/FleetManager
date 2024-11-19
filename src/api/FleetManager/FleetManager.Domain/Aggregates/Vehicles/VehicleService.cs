using FleetManager.Domain.Aggregates.Locations;
using FleetManager.Domain.SeedWork.Results;

namespace FleetManager.Domain.Aggregates.Vehicles
{
    public sealed class VehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Result> AddNewVehicleAsync(string vin,
            string licensePlate,
            string model,
            DateTime lastInspectionDate,
            DateTime nextInspectionDate,
            Location? currentLocation,
            List<Inspection>? inspections = null,
            List<Repair>? repairs = null,
            CancellationToken cancellationToken = new())
        {
            var result = VehicleFactory.Create(vin, licensePlate, model, lastInspectionDate, nextInspectionDate,
                currentLocation, inspections, repairs);

            if (result.IsFailure  || result.Value is null)
            {
                return result;
            }

            Vehicle newVehicle = result.Value;

            Vehicle? existingVehicle =
                await _vehicleRepository.GetByVehicleDetailsAsync(newVehicle.VehicleDetails, cancellationToken);

            if (existingVehicle is not null)
            {
                return Result.Failure(VehicleErrors.VehicleAlreadyExists(existingVehicle.VehicleDetails));
            }

            await _vehicleRepository.AddAsync(newVehicle, cancellationToken);

            return Result.Success();
        }
    }
}
