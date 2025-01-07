namespace FleetManager.Application.Routes.GetRoutePlannerForm.Dto
{
    public record RoutePlannerFormDto(
        List<FormLocationDto> Locations,
        List<UserDto> Users,
        List<VehicleDto> Vehicles)
    {
    }
}
