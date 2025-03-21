using FleetManager.BuildingBlocks.Domain.Results;

namespace FleetManager.Modules.Orders.Domain.Orders
{
    internal static class OrderFactory
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
                return Result<Order>.Failure(OrderErrors.MissingContractor());
            }

            if (originId == Guid.Empty)
            {
                return Result<Order>.Failure(OrderErrors.MissingOriginLocation());

            }

            if (destinationId == Guid.Empty)
            {
                return Result<Order>.Failure(OrderErrors.MissingDestinationLocation());

            }

            if (pickupDate == default)
            {
                return Result<Order>.Failure(OrderErrors.InvalidPickupDate());
            }

            if (deliveryDate == default)
            {
                return Result<Order>.Failure(OrderErrors.InvalidDeliveryDate());
            }

            DateTime currentDate = DateTime.UtcNow;

            if (pickupDate < currentDate)
            {
                return Result<Order>.Failure(OrderErrors.PickupDateInThePast());
            }

            if (deliveryDate < pickupDate)
            {
                return Result<Order>.Failure(OrderErrors.DeliveryDateIsEarlierThanPickupDate());
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
