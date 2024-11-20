using FleetManager.Domain.Vehicles.Models;
using MediatR;

namespace FleetManager.Application.Vehicles.Queries.GetVehicles
{
    public sealed record GetVehiclesQuery : IRequest<IEnumerable<Vehicle>>
    {
    }
}
