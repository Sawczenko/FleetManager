namespace FleetManager.Application.Orders.GetOrders
{
    public record OrdersFilterDto(
        Guid? ContractorId, 
        Guid? OriginLocationId, 
        Guid? DestinationLocationId,
        DateTime PickupDateFrom,
        DateTime PickupDateTo,
        DateTime DeliveryDateFrom,
        DateTime DeliveryDateTo)
    {
    }
}
