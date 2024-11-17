namespace FleetManager.Domain.Aggregates.Vehicles
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
            Id = Guid.NewGuid();
            VehicleId = vehicleId;
            Date = date;
            Description = description;
            Cost = cost;
        }
    }
}
