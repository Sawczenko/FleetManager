using FleetManager.Domain.Vehicles.Models;

namespace FleetManager.Application.Vehicles.GetVehiclesWithUpcomingMaintenances
{
    public record VehicleWithUpcomingMaintenanceDto(DateTime NextInspectionDate, string Vin, string LicensePlate, string Model)
    {
        public VehicleWithUpcomingMaintenanceDto(DateTime nextInspectionDate, VehicleDetails vehicleDetails) : this(nextInspectionDate, vehicleDetails.Vin, vehicleDetails.LicensePlate, vehicleDetails.Model)
        {

        }
    }
}
