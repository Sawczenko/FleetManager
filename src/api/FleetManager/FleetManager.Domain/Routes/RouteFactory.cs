using FleetManager.Domain.SeedWork.Results;

namespace FleetManager.Domain.Routes;

public static class RouteFactory
{
    public static Result<Route> Create(Guid startLocationId, Guid endLocationId, Guid userId, Guid vehicleId)
    {
        return Result<Route>.Success(new Route(startLocationId, endLocationId, userId, vehicleId));
    }
}