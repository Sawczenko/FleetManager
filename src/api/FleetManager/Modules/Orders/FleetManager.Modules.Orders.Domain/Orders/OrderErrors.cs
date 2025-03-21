using FleetManager.BuildingBlocks.Domain;

namespace FleetManager.Modules.Orders.Domain.Orders;

public class OrderErrors
{
    public static Error MissingContractor() => new Error("Contractor.MissingContractor", "Contractor is missing.");

    public static Error MissingOriginLocation() =>
        new Error("Contractor.MissingOriginLocation", "Origin location is missing.");

    public static Error MissingDestinationLocation() =>
        new Error("Contractor.MissingDestinationLocation", "Destination location is missing.");

    public static Error InvalidPickupDate() => new Error("Contractor.InvalidPickupDate", "Pickup date is invalid.");

    public static Error InvalidDeliveryDate() =>
        new Error("Contractor.InvalidDeliveryDate", "Delivery date is invalid.");

    public static Error PickupDateInThePast() =>
        new Error("Contractor.PickupDateInThePast", "Pickup date cannot be in the past.");

    public static Error DeliveryDateIsEarlierThanPickupDate() =>
        new Error("Contractor.DeliveryDateIsEarlierThanPickupDate", "Delivery date cannot be earlier than pickup date.");
}
