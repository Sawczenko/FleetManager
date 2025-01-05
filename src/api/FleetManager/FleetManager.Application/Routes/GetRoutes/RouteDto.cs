namespace FleetManager.Application.Routes.GetRoutes
{
    public record RouteDto(
        string UserName, 
        LocationDto StartLocation,
        LocationDto EndLocation,
        string Vehicle, 
        DateTime ScheduledStartTime,
        DateTime? EndTime,
        string Status)
    {

    }
}
