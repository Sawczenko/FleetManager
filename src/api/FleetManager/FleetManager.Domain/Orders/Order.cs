namespace FleetManager.Domain.Orders
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid ContractorId { get; private set; }
        public Guid OriginId { get; private set; }
        public Guid DestinationId { get; private set; }
        public DateTime PickupDate { get; private set; }
        public DateTime DeliveryDate { get; private set; }
        public OrderStatus Status { get; private set; }

        internal Order(
            Guid contractorId,
            Guid originId,
            Guid destinationId,
            DateTime pickupDate,
            DateTime deliveryDate)
        {
            ContractorId = contractorId;
            OriginId = originId;
            DestinationId = destinationId;
            PickupDate = pickupDate;
            DeliveryDate = deliveryDate;
            Status = OrderStatus.Created;
        }
    }
}
