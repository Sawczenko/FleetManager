﻿using FleetManager.Domain.Locations;

namespace FleetManager.Domain.Vehicles.Models
{
    public class Vehicle
    {
        public Guid Id { get; private set; }
        public VehicleDetails VehicleDetails { get; private set; }
        public DateTime LastInspectionDate { get; private set; }
        public DateTime NextInspectionDate { get; private set; }
        public Location? CurrentLocation { get; private set; }
        public List<Inspection> Inspections { get; private set; }
        public List<Repair> Repairs { get; private set; }

        private Vehicle()
        {
            Inspections = new List<Inspection>();
            Repairs = new List<Repair>();
            VehicleDetails = new VehicleDetails(string.Empty, string.Empty, string.Empty);
        }

        internal Vehicle(VehicleDetails vehicleDetails,
            DateTime lastInspectionDate,
            DateTime nextInspectionDate,
            Location currentLocation)
        {
            Id = Guid.NewGuid();
            VehicleDetails = vehicleDetails;
            LastInspectionDate = lastInspectionDate;
            NextInspectionDate = nextInspectionDate;
            CurrentLocation = currentLocation;
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
