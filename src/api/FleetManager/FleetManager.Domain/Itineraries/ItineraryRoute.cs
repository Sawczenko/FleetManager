using FleetManager.Domain.Routes;

namespace FleetManager.Domain.Itineraries
{
    public class ItineraryRoute
    {
        public Guid Id { get; set; }

        public Guid ItineraryId { get; private set; }

        public Guid RouteId { get; private set; }

        public int Order { get; private set; }

        public ItineraryRoute(Guid itineraryId, Guid routeId, int order)
        {
            ItineraryId = itineraryId;
            RouteId = routeId;
            Order = order;
        }
    }
}
