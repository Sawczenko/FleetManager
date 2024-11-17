namespace FleetManager.Domain.Aggregates.VehicleUsages
{
    public record FuelExpense
    {
        public Guid Id { get; private set; }
        public Guid VehicleUsageId { get; private set; }
        public Guid RouteId { get; private set; }
        public double Amount { get; private set; }
        public double Liters { get; private set; }
        public FuelType FuelType { get; private set; }
        public DateTime Date { get; private set; }

        public FuelExpense(Guid vehicleUsageId, Guid routeId, double amount, double liters, FuelType fuelType)
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
