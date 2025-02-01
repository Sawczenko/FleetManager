namespace FleetManager.Domain.Itineraries;

public interface IItineraryRepository
{
    public Task AddAsync(Itinerary itinerary, CancellationToken cancellationToken);
}