namespace FleetManager.Domain.Aggregates.Routes
{
    public class Route
    {
        public Guid Id { get; private set; }
        public Guid StartLocationId { get; private set; }
        public Guid EndLocationId { get; private set; }
        public RouteStatus Status { get; private set; }
        public DateTime ScheduledStartTime { get; private set; }
        public DateTime? ActualEndTime { get; private set; }
        public List<RouteStop> RouteStops { get; private set; }
        public Guid VehicleUsageId { get; private set; }

        public Route(Guid startLocationId, Guid endLocationId, Guid vehicleUsageId)
        {
            Id = Guid.NewGuid();
            StartLocationId = startLocationId;
            EndLocationId = endLocationId;
            Status = RouteStatus.Planned;
            ScheduledStartTime = DateTime.UtcNow;
            RouteStops = new List<RouteStop>();
            VehicleUsageId = vehicleUsageId; // Ustawienie klucza obcego
        }

        public void AddStop(RouteStop stop)
        {
            RouteStops.Add(stop);
        }

        public void CompleteRoute()
        {
            Status = RouteStatus.Completed;
            ActualEndTime = DateTime.UtcNow;
        }
    }

}
