using MediatR;

namespace FleetManager.Application.Vehicles.GetVehicles
{
    public sealed record GetVehiclesQuery : IRequest<IEnumerable<VehicleDto>>
    {
    }
}
