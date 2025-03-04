using FleetManager.BuildingBlocks.Domain.Results;

namespace FleetManager.Modules.Orders.Domain
{
    public static class OrderFactory
    {
        public static Result<Order> Create(
            Guid contractorId,
            Guid originId,
            Guid destinationId,
            DateTime pickupDate,
            DateTime deliveryDate)
        {
            if (contractorId == Guid.Empty)
            {
                return Result<Order>.Failure(Errors.MissingContractor());
            }

            if (originId == Guid.Empty)
            {
                return Result<Order>.Failure(Errors.MissingOriginLocation());

            }

            if (destinationId == Guid.Empty)
            {
                return Result<Order>.Failure(Errors.MissingDestinationLocation());

            }

            if (pickupDate == default)
            {
                return Result<Order>.Failure(Errors.InvalidPickupDate());
            }

            if (deliveryDate == default)
            {
                return Result<Order>.Failure(Errors.InvalidDeliveryDate());
            }

            DateTime currentDate = DateTime.UtcNow;

            if (pickupDate < currentDate)
            {
                return Result<Order>.Failure(Errors.PickupDateInThePast());
            }

            if (deliveryDate < pickupDate)
            {
                return Result<Order>.Failure(Errors.DeliveryDateIsEarlierThanPickupDate());
            }

            return Result<Order>.Success(new Order(
                contractorId,
                originId,
                destinationId,
                pickupDate,
                deliveryDate));
        }
    }
}
