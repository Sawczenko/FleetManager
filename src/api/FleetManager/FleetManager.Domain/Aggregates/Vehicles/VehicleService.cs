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
            if (string.IsNullOrWhiteSpace(vin))
            {
                return Result.Failure(VehicleErrors.MissingVehicleDetails(nameof(vin)));
            }

            if (string.IsNullOrWhiteSpace(licensePlate))
            {
                return Result.Failure(VehicleErrors.MissingVehicleDetails(nameof(licensePlate)));
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                return Result.Failure(VehicleErrors.MissingVehicleDetails(nameof(model)));
            }

            if (currentLocation is null)
            {
                return Result.Failure(VehicleErrors.MissingInitialLocation);
            }

            DateTime currentDate = DateTime.UtcNow;
;
            if (lastInspectionDate > currentDate)
            {
                return Result.Failure(VehicleErrors.FutureLastInspectionDate(lastInspectionDate, currentDate));
            }

            if (nextInspectionDate < currentDate)
            {
                return Result.Failure(VehicleErrors.PastNextInspectionDate(nextInspectionDate, currentDate));
            }

            if (nextInspectionDate < lastInspectionDate)
            {
                return Result.Failure(VehicleErrors.NextInspectionDateOlderThanLastInspectionDate(nextInspectionDate, lastInspectionDate));
            }

            Vehicle vehicle = new Vehicle(vin,
                licensePlate, 
                model,
                lastInspectionDate, 
                nextInspectionDate,
                currentLocation,
                inspections,
                repairs);

            Vehicle? existingVehicle =
                await _vehicleRepository.GetByVehicleDataAsync(vehicle.VehicleData, cancellationToken);

            if (existingVehicle is not null)
            {
                return Result.Failure(VehicleErrors.VehicleAlreadyExists(existingVehicle.VehicleData));
            }

            await _vehicleRepository.AddAsync(vehicle, cancellationToken);

            return Result.Success();
        }
    }
}
