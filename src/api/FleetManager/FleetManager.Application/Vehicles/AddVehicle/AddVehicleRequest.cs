namespace FleetManager.Application.Vehicles.AddVehicle
{
    public record AddVehicleRequest(
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
