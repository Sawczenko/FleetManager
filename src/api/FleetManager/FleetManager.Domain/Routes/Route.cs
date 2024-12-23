﻿namespace FleetManager.Domain.Routes
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

        internal Route(Guid startLocationId, Guid endLocationId)
        {
            Id = Guid.NewGuid();
            StartLocationId = startLocationId;
            EndLocationId = endLocationId;
            Status = RouteStatus.Planned;
            ScheduledStartTime = DateTime.UtcNow;
            RouteStops = new List<RouteStop>();
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
