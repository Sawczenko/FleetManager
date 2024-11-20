using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.Vehicles.Models;
using FleetManager.Domain.Locations;

namespace FleetManager.Domain.Vehicles
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
            CancellationToken cancellationToken = new())
        {
            var result = VehicleFactory.Create(vin, licensePlate, model, lastInspectionDate, nextInspectionDate,
                currentLocation);

            if (result.IsFailure)
            {
                return result;
            }

            if (result.Value is null)
            {
                return Result.Failure(VehicleErrors.VehicleWasNotCreated);
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
