namespace FleetManager.Application.Vehicles.Shared
{
    public record RepairDto
    {
        public Guid VehicleId { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public double Cost { get; set; }

        public RepairDto(DateTime date, string description, double cost)
        {
            Date = date;
            Description = description;
            Cost = cost;
        }
    }
}
