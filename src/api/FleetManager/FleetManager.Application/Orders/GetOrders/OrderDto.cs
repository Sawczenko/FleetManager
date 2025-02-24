using FleetManager.Domain.Contractors;
using FleetManager.Domain.Locations;

namespace FleetManager.Application.Orders.GetOrders
{
    public record OrderDto(
        Guid Id,
        ContractorInfo Contractor, 
        LocationInfo OriginLocation, 
        LocationInfo DestinationLocation,
        DateTime PickupDate,
        DateTime DeliveryDate,
        string Status
    )
    {
    }
}
