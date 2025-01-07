namespace FleetManager.Application.Routes.AddRoute
{
    public record AddRouteRequest(
        string UserId,
        string VehicleId,
        DateTime ScheduledStartTime,
        string StartLocationId,
        string EndLocationId
        )
    {
    }
}
