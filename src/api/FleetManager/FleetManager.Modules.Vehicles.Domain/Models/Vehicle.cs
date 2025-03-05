namespace FleetManager.Modules.Vehicles.Domain.Models
{
    public class Vehicle
    {
        public Guid Id { get; private set; }
        public VehicleStatus Status { get; private set; }
        public VehicleDetails VehicleDetails { get; private set; }
        public DateTime LastInspectionDate { get; private set; }
        public DateTime NextInspectionDate { get; private set; }
        public Guid? CurrentLocationId { get; private set; }
        public List<Inspection> Inspections { get; private set; }
        public List<Repair> Repairs { get; private set; }

        private Vehicle()
        {
            Inspections = new List<Inspection>();
            Repairs = new List<Repair>();
            VehicleDetails = new VehicleDetails(string.Empty, string.Empty, string.Empty);
            Status = VehicleStatus.Available;
        }

        internal Vehicle(VehicleDetails vehicleDetails,
            DateTime lastInspectionDate,
            DateTime nextInspectionDate,
            Guid currentLocationId,
            VehicleStatus status)
        {
            Status = VehicleStatus.Available;
            VehicleDetails = vehicleDetails;
            LastInspectionDate = lastInspectionDate;
            NextInspectionDate = nextInspectionDate;
            CurrentLocationId = currentLocationId;
            Inspections = new List<Inspection>();
            Repairs = new List<Repair>();
            Status = status;
        }

        public void UpdateLocation(Guid newLocationId)
        {
            CurrentLocationId = newLocationId;
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
