using FleetManager.BuildingBlocks.Domain.Results;
using FleetManager.Modules.Vehicles.Domain.Models;

namespace FleetManager.Modules.Vehicles.Domain
{
    public static class VehicleFactory
    {
        public static Result<Vehicle> Create(string vin,
            string licensePlate,
            string model,
            DateTime lastInspectionDate,
            DateTime nextInspectionDate,
            Guid currentLocationId,
            VehicleStatus vehicleStatus = VehicleStatus.Available)
        {
            if (string.IsNullOrWhiteSpace(vin))
            {
                return Result<Vehicle>.Failure(VehicleErrors.MissingVehicleDetails(nameof(vin)));
            }

            if (string.IsNullOrWhiteSpace(licensePlate))
            {
                return Result<Vehicle>.Failure(VehicleErrors.MissingVehicleDetails(nameof(licensePlate)));
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                return Result<Vehicle>.Failure(VehicleErrors.MissingVehicleDetails(nameof(model)));
            }

            if (currentLocationId == Guid.Empty)
            {
                return Result<Vehicle>.Failure(VehicleErrors.MissingInitialLocation);
            }

            DateTime currentDate = DateTime.UtcNow;

            if (lastInspectionDate > currentDate)
            {
                return Result<Vehicle>.Failure(VehicleErrors.FutureLastInspectionDate(lastInspectionDate, currentDate));
            }

            if (nextInspectionDate < currentDate)
            {
                return Result<Vehicle>.Failure(VehicleErrors.PastNextInspectionDate(nextInspectionDate, currentDate));
            }

            Vehicle vehicle = new Vehicle(new VehicleDetails(vin, licensePlate, model),
                lastInspectionDate,
                nextInspectionDate,
                currentLocationId,
                vehicleStatus);

            return Result<Vehicle>.Success(vehicle);
        }
    }
}
