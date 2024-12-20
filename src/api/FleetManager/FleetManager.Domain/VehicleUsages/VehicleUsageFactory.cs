using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.Vehicles.Models;

namespace FleetManager.Domain.VehicleUsages
{
    public static class VehicleUsageFactory
    {
        public static Result<VehicleUsage> Create(Guid vehicleId, Guid routeId, Guid userId, DateTime startDate)
        {
            return Result<VehicleUsage>.Success(new VehicleUsage(vehicleId, routeId, userId, startDate));
        }
    }
}
