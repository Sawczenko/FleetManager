namespace FleetManager.Modules.Orders.Domain;

public enum OrderStatus
{
    Created = 1,
    Approved = 2,
    Planned = 3,
    AwaitingPickup = 4,
    InTransit = 5,
    Delivered = 6,
    Completed = 7,
}