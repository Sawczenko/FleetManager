using FleetManager.Domain.Itineraries.Checkpoints;
using FleetManager.Domain.SeedWork.Results;

namespace FleetManager.Domain.Itineraries
{
    public static class ItineraryFactory
    {
        public static Result<Itinerary> Create(
            List<OrderRouting> orderRoutings,
            Guid driverId,
            Guid vehicleId,
            DateTime scheduledStartDate,
            DateTime scheduledEndDate)
        {
            if (!orderRoutings.Any())
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

            List<Checkpoint> checkpoints = CreateCheckpoints(orderRoutings);

            return Result<Itinerary>.Success(new Itinerary(
                checkpoints,
                driverId,
                vehicleId,
                scheduledStartDate,
                scheduledEndDate));
        }

        private static List<Checkpoint> CreateCheckpoints(List<OrderRouting> orderRoutings)
        {
            List<Checkpoint> checkpoints = new List<Checkpoint>();

            foreach (var order in orderRoutings.OrderBy(x => x.Sequence))
            {
                Checkpoint pickupCheckpoint = new Checkpoint(order.OrderId, order.PickupLocationId, CheckpointType.Pickup);
                Checkpoint deliveryCheckpoint = new Checkpoint(order.OrderId, order.DeliveryLocationId, CheckpointType.Delivery);

                checkpoints.Add(pickupCheckpoint);
                checkpoints.Add(deliveryCheckpoint);
            }

            return checkpoints;
        }
    }
}
