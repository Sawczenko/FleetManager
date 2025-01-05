namespace FleetManager.Application.Routes.GetRoutes
{
    public record RoutesFilterDto(
        string? UserName,
        string? StartLocation,
        string? EndLocation,
        string? Status,
        DateTime? ScheduledStartTime,
        DateTime? EndTime
        )
    {
    }
}
