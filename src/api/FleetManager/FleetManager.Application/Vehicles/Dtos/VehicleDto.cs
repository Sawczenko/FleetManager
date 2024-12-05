using FleetManager.Domain.Vehicles.Models;
using System.Text.Json.Serialization;

namespace FleetManager.Application.Vehicles.Dtos
{
    [method: JsonConstructor]
    public record VehicleDto(Guid Id, string Vin, string LicensePlate, string Model)
    {

        public VehicleDto(Guid id, VehicleDetails vehicleDetails) : this(id, vehicleDetails.Vin,
            vehicleDetails.LicensePlate, vehicleDetails.Model)
        {
        }
    }
}
