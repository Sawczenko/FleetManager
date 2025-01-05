namespace FleetManager.Application.Vehicles.GetVehicles
{
    public record VehiclesFilterDto(string? Vin, string? LicensePlate, string? Model)
    {
    }
}
