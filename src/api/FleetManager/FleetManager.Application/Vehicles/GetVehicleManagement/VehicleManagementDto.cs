using FleetManager.Application.Vehicles.Shared;
using FleetManager.Domain.Vehicles.Models;
using System.Text.Json.Serialization;

namespace FleetManager.Application.Vehicles.GetVehicleManagement;

[method: JsonConstructor]
public record VehicleManagementDto(
    Guid Id,
    string Vin,
    string LicensePlate,
    string Model,
    string Status,
    DateTime NextInspectionDate,
    List<InspectionDto> Inspections,
    List<RepairDto> Repairs)
{
    public VehicleManagementDto(Guid id, VehicleDetails vehicleDetails, string status, DateTime nextInspectionDate, List<InspectionDto> inspections, List<RepairDto> repairs) : this(id, vehicleDetails.Vin,
        vehicleDetails.LicensePlate, vehicleDetails.Model, status, nextInspectionDate, inspections, repairs)
    {
    }
}