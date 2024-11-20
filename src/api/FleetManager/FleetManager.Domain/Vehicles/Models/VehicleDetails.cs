namespace FleetManager.Domain.Vehicles.Models;

public record VehicleDetails
{
    public string Vin { get; private set; }
    public string LicensePlate { get; private set; }
    public string Model { get; private set; }

    public VehicleDetails(string vin, string licensePlate, string model)
    {
        Vin = vin;
        LicensePlate = licensePlate;
        Model = model;
    }
}