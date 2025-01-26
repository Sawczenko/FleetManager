using FleetManager.Domain.SeedWork.Results;

namespace FleetManager.Domain.Itinerary
{
    public static class ItineraryFactory
    {
        public static Result<Itinerary> Create(
            Guid routeId, 
            Guid driverId, 
            Guid vehicleId, 
            DateTime scheduledStartDate, 
            DateTime scheduledEndDate)
        {
            if (routeId == Guid.Empty)
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
                routeId, 
                driverId, 
                vehicleId, 
                scheduledStartDate,
                scheduledEndDate));
        }
    }
}
