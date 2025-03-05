namespace FleetManager.Modules.Vehicles.Domain.Models
{
    public record Repair
    {
        public Guid Id { get; private set; }
        public Guid VehicleId { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
        public double Cost { get; private set; }

        public Repair(Guid vehicleId, DateTime date, string description, double cost)
        {
            VehicleId = vehicleId;
            Date = date;
            Description = description;
            Cost = cost;
        }
    }
}
