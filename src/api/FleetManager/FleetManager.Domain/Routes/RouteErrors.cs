using FleetManager.Domain.SeedWork;

namespace FleetManager.Domain.Routes
{
    public class RouteErrors
    {
        public static Error MissingLocation() => new Error("Route.MissingLocation", $"Start or end location is missing.");
    }
}
