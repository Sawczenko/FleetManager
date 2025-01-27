using FleetManager.Domain.SeedWork.Results;

namespace FleetManager.Domain.Itineraries
{
    public static class ItineraryFactory
    {
        public static Result<Itinerary> Create(
            List<ItineraryRoute> routes,
            Guid driverId,
            Guid vehicleId,
            DateTime scheduledStartDate,
            DateTime scheduledEndDate)
        {
            if (routes.Any(x => x.RouteId == Guid.Empty))
            {
                return Result<Itinerary>.Failure(ItineraryErrors.MissingRoute());
            }

            if (driverId == Guid.Empty)
            {
                return Result<Itinerary>.Failure(ItineraryErrors.MissingDriver());
            }

            if (vehicleId == Guid.Empty)
            {
                return Result<Itinerary>.Failure(ItineraryErrors.MissingVehicle());
            }

            if (scheduledStartDate < DateTime.UtcNow)
            {
                return Result<Itinerary>.Failure(ItineraryErrors.ScheduledStartDateCannotBeInThePast());
            }

            if (scheduledStartDate > scheduledEndDate)
            {
                return Result<Itinerary>.Failure(ItineraryErrors.ScheduledStartDateCannotBeLaterThanScheduledEndDate());
            }

            return Result<Itinerary>.Success(new Itinerary(
                routes,
                driverId,
                vehicleId,
                scheduledStartDate,
                scheduledEndDate));
        }
    }
}
