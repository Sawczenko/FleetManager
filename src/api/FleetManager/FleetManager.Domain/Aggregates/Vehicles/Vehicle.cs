using FleetManager.Domain.Aggregates.Locations;

namespace FleetManager.Domain.Aggregates.Vehicles
{
    public class Vehicle
    {
        public Guid Id { get; private set; }
        public string Vin { get; private set; }
        public string LicensePlate { get; private set; }
        public string Model { get; private set; }
        public DateTime LastInspectionDate { get; private set; }
        public DateTime NextInspectionDate { get; private set; }
        public Location CurrentLocation { get; private set; }
        public List<Inspection> Inspections { get; private set; }
        public List<Repair> Repairs { get; private set; }

        internal Vehicle(string vin,
            string licensePlate,
            string model,
            DateTime lastInspectionDate,
            DateTime nextInspectionDate,
            Location currentLocation,
            List<Inspection>? inspections = null,
            List<Repair>? repairs = null)
        {
            Id = Guid.NewGuid();
            Vin = vin;
            LicensePlate = licensePlate;
            Model = model;
            LastInspectionDate = lastInspectionDate;
            NextInspectionDate = nextInspectionDate;
            CurrentLocation = currentLocation;
            Inspections = inspections ?? new List<Inspection>();
            Repairs = repairs ?? new List<Repair>();
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
