namespace FleetManager.Domain.VehicleUsages
{
    public record FuelExpense
    {
        public Guid Id { get; private set; }
        public Guid VehicleUsageId { get; private set; }
        public Guid RouteId { get; private set; }
        public decimal Amount { get; private set; }
        public decimal Liters { get; private set; }
        public FuelType FuelType { get; private set; }
        public DateTime Date { get; private set; }

        public FuelExpense(Guid vehicleUsageId, Guid routeId, decimal amount, decimal liters, FuelType fuelType)
        {
            Id = Guid.NewGuid();
            VehicleUsageId = vehicleUsageId;
            RouteId = routeId;
            Amount = amount;
            Liters = liters;
            FuelType = fuelType;
            Date = DateTime.UtcNow;
        }
    }

}
