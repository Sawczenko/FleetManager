namespace FleetManager.Domain.Routes
{
    public class RouteStop
    {
        public Guid Id { get; private set; }
        public Guid RouteId { get; private set; }
        public Guid LocationId { get; private set; }
        public DateTime ArrivalTime { get; private set; }
        public DateTime DepartureTime { get; private set; }

        public RouteStop(Guid routeId, Guid locationId)
        {
            Id = Guid.NewGuid();
            RouteId = routeId;
            LocationId = locationId;
        }
    }
}
