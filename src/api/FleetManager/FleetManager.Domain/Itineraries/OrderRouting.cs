namespace FleetManager.Domain.Itineraries
{
    public record OrderRouting(Guid OrderId, Guid PickupLocationId, Guid DeliveryLocationId, int Sequence)
    {
    }
}
