using FleetManager.Application.Vehicles.Dtos;
using MediatR;

namespace FleetManager.Application.Vehicles.Queries.GetVehicles
{
    public sealed record GetVehiclesQuery : IRequest<IEnumerable<VehicleDto>>
    {
    }
}
