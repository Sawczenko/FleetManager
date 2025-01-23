namespace FleetManager.Domain.Itinerary;

public class Itinerary
{
    public Guid Id { get; private set; }

    public Guid RouteId { get; private set; }

    public Guid UserId { get; private set; }

    public Guid VehicleId { get; private set; }

    public DateTime ScheduledStartDate { get; private set; }

    public DateTime ScheduledEndDate { get; private set; }

    public DateTime? ActualEndTime { get; private set; }

    public ItineraryStatus Status { get; private set; }
}