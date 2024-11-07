using FleetManager.Domain.Locations;

namespace FleetManager.Domain.Vehicles
{
    public class Vehicle
    {
        public Guid Id { get; private set; }
        public string Vin { get; private set; }
        public string LicensePlate { get; private set; }
        public string Model { get; private set; }
        public DateTime LastInspectionDate { get; private set; }
        public Location CurrentLocation { get; private set; }
        public List<Inspection> Inspections { get; private set; }
        public List<Repair> Repairs { get; private set; }

        public Vehicle(string vin, string licensePlate, string model)
        {
            Id = Guid.NewGuid();
            Vin = vin;
            LicensePlate = licensePlate;
            Model = model;
            LastInspectionDate = DateTime.UtcNow;
            Inspections = new List<Inspection>();
            Repairs = new List<Repair>();
        }

        public void UpdateLocation(Location newLocation)
        {
            CurrentLocation = newLocation;
        }

        public void AddInspection(Inspection inspection)
        {
            Inspections.Add(inspection);
            LastInspectionDate = inspection.Date;
        }

        public void AddRepair(Repair repair)
        {
            Repairs.Add(repair);
        }
    }

}
