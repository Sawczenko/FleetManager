namespace FleetManager.Modules.Itineraries.Domain;

public interface IItineraryRepository
{
    public Task AddAsync(Itinerary itinerary, CancellationToken cancellationToken);
}