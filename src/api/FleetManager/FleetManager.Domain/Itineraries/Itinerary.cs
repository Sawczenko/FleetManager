namespace FleetManager.Domain.Itineraries;

public class Itinerary
{
    public Guid Id { get; private set; }

    public List<ItineraryRoute> Routes { get; private set; }

    public Guid DriverId { get; private set; }

    public Guid VehicleId { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public ItineraryStatus Status { get; private set; }

    private Itinerary()
    {
        Routes = new List<ItineraryRoute>();
    }

    internal Itinerary(List<ItineraryRoute> routes, Guid driverId, Guid vehicleId, DateTime startDate, DateTime endDate)
    {
        Routes = routes;
        DriverId = driverId;
        VehicleId = vehicleId;
        StartDate = startDate;
        EndDate = endDate;
        Status = ItineraryStatus.Planned;
    }
}