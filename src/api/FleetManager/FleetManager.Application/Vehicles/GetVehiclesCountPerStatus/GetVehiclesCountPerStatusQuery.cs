using FleetManager.Domain.Vehicles.Models;
using FleetManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Application.Vehicles.GetVehiclesCountPerStatus;

public class GetVehiclesCountPerStatusQuery : IRequest<Dictionary<VehicleStatus, int>>
{

}

public class GetVehiclesCountPerStatusQueryHandler : IRequestHandler<GetVehiclesCountPerStatusQuery, Dictionary<VehicleStatus, int>>
{
    private readonly FleetManagerDbContext _dbContext;

    public GetVehiclesCountPerStatusQueryHandler(FleetManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Dictionary<VehicleStatus, int>> Handle(GetVehiclesCountPerStatusQuery request, CancellationToken cancellationToken)
    {
        return _dbContext.Set<Vehicle>()
            .GroupBy(x => x.Status)
            .Select(x => new
            {
                Status = x.Key,
                Count = x.Count(),
            })
            .ToDictionaryAsync(x => x.Status, x => x.Count, cancellationToken);
    }
}