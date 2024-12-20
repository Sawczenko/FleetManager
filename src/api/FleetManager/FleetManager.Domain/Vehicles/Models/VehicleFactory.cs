using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.Locations;

namespace FleetManager.Domain.Vehicles.Models
{
    public static class VehicleFactory
    {
        public static Result<Vehicle> Create(string vin,
            string licensePlate,
            string model,
            DateTime lastInspectionDate,
            DateTime nextInspectionDate,
            Location? currentLocation,
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

            if (currentLocation is null)
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

            if (nextInspectionDate < lastInspectionDate)
            {
                return Result<Vehicle>.Failure(VehicleErrors.NextInspectionDateOlderThanLastInspectionDate(nextInspectionDate, lastInspectionDate));
            }

            Vehicle vehicle = new Vehicle(new VehicleDetails(vin, licensePlate, model),
                lastInspectionDate,
                nextInspectionDate,
                currentLocation,
                vehicleStatus);

            return Result<Vehicle>.Success(vehicle);
        }
    }
}
