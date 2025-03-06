namespace FleetManager.Modules.Itineraries.Domain
{
    public record OrderRouting(Guid OrderId, Guid PickupLocationId, Guid DeliveryLocationId, int Sequence)
    {
    }
}
