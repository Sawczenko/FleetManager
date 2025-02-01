namespace FleetManager.Application.Orders.CreateOrder
{
    public record CreateOrderCommandRequest(
        string ContractorId,
        string OriginId,
        string DestinationId,
        DateTime PickupDate,
        DateTime DeliveryDate
        )
    {
    }
}
