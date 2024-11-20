using FleetManager.Domain.SeedWork;
using FleetManager.Domain.Vehicles.Models;

namespace FleetManager.Domain.Vehicles
{
    public static class VehicleErrors
    {
        public static Error MissingVehicleDetails(string parameterName) => new Error("Vehicle.MissingVehicleDetails", $"Missing vehicle detail - {parameterName}");

        public static readonly Error MissingInitialLocation = new Error("Vehicle.MissingInitialLocation", "Initial vehicle location is missing.");

        public static Error FutureLastInspectionDate(DateTime lastInspectionDate, DateTime currentDate) => new Error("Vehicle.FutureLastInspectionDate", $"Last inspection date ({lastInspectionDate}) cannot be in the future. Current date {currentDate}");

        public static Error PastNextInspectionDate(DateTime nextInspectionDate, DateTime currentDate) =>
            new Error("Vehicle.PastNextInspectionDate", $"Next inspection date ({nextInspectionDate}) cannot be in the past. Current date {currentDate}.");

        public static Error NextInspectionDateOlderThanLastInspectionDate(DateTime nextInspectionDate,
            DateTime lastInspectionDate) => new Error("Vehicle.NextInspectionDateOlderThanLastInspectionDate", $"Next inspection date ({nextInspectionDate}) cannot be older than last inspection date ({lastInspectionDate}).");

        public static Error VehicleAlreadyExists(VehicleDetails vehicleDetails) =>
            new Error("Vehicle.VehicleAlreadyExists", $"Vehicle with VIN: {vehicleDetails.Vin}, LicensePlate: {vehicleDetails.LicensePlate} already exists.");

        public static readonly Error VehicleWasNotCreated =
            new Error("Vehicle.VehicleWasNotCreated", $"Vehicle was not created due to an error.");
    }
}
