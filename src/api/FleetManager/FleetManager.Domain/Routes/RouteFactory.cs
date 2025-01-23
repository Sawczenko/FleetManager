using FleetManager.Domain.SeedWork.Results;

namespace FleetManager.Domain.Routes;

public static class RouteFactory
{
    public static Result<Route> Create(Guid startLocationId, Guid endLocationId)
    {
        if (startLocationId == Guid.Empty || endLocationId == Guid.Empty)
        {
            return Result<Route>.Failure(RouteErrors.MissingLocation());
        }

        return Result<Route>.Success(new Route(startLocationId, endLocationId));
    }
}