namespace FleetManager.Application.Orders.GetOrders
{
    public record OrderDto(
        Guid Id,
        string ContractorName, 
        string OriginLocation, 
        string DestinationLocation,
        DateTime PickupDate,
        DateTime DeliveryDate,
        string Status
    )
    {
    }
}
