namespace FleetManager.Application.Vehicles.CreateVehicle
{
    public record CreateVehicleRequest(
        string Vin,
        string LicensePlate,
        string Model,
        DateTime LastInspectionDate,
        DateTime NextInspectionDate,
        string LocationName,
        double Latitude,
        double Longitude)
    {
    }
}
