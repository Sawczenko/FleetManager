using FleetManager.Domain.SeedWork.Results;

namespace FleetManager.Domain.Routes;

public static class RouteFactory
{
    public static Result<Route> Create(Guid startLocationId, Guid endLocationId)
    {
        return Result<Route>.Success(new Route(startLocationId, endLocationId));
    }
}