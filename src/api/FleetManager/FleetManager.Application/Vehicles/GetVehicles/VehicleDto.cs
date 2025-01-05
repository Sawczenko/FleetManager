using FleetManager.Domain.Vehicles.Models;
using System.Text.Json.Serialization;

namespace FleetManager.Application.Vehicles.GetVehicles
{
    [method: JsonConstructor]
    public record VehicleDto(Guid Id, string Vin, string LicensePlate, string Model, string Status)
    {

        public VehicleDto(Guid id, VehicleDetails vehicleDetails, string status) : this(id, vehicleDetails.Vin,
            vehicleDetails.LicensePlate, vehicleDetails.Model, status)
        {
        }
    }
}
