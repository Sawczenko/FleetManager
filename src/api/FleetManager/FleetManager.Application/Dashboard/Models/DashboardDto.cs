using FleetManager.Application.Vehicles.GetVehiclesWithUpcomingMaintenances;
using FleetManager.Domain.Routes;
using FleetManager.Domain.Vehicles.Models;

namespace FleetManager.Application.Dashboard.Models
{
    public class DashboardDto
    {
        public Dictionary<VehicleStatus, int> VehiclesCountPerStatus { get; set; }
        public List<VehicleWithUpcomingMaintenanceDto> VehiclesWithUpcomingMaintenance { get; set; }
        public Dictionary<RouteStatus, int> RoutesCountPerStatus { get; set; }

        public DashboardDto(Dictionary<VehicleStatus, int> vehiclesCountPerStatus,
            List<VehicleWithUpcomingMaintenanceDto> vehiclesWithUpcomingMaintenance,
            Dictionary<RouteStatus, int> routesCountPerStatus)
        {
            VehiclesCountPerStatus = vehiclesCountPerStatus;
            VehiclesWithUpcomingMaintenance = vehiclesWithUpcomingMaintenance;
            RoutesCountPerStatus = routesCountPerStatus;
        }
    }
}
