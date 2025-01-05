using MediatR;

namespace FleetManager.Application.Vehicles.GetVehicles
{
    public sealed record GetVehiclesQuery(VehiclesFilterDto VehiclesFilter) : IRequest<IEnumerable<VehicleDto>>
    {
    }
}
