using FleetManager.Domain.Vehicles.Models;
using FleetManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Application.Vehicles.GetVehiclesWithUpcomingMaintenances
{
    internal record GetVehiclesWithUpcomingMaintenancesQuery : IRequest<List<VehicleWithUpcomingMaintenanceDto>>
    {
    }

    internal class GetVehiclesWithUpcomingMaintenancesQueryHandler : IRequestHandler<GetVehiclesWithUpcomingMaintenancesQuery, List<VehicleWithUpcomingMaintenanceDto>>
    {
        private readonly FleetManagerDbContext _dbContext;

        public GetVehiclesWithUpcomingMaintenancesQueryHandler(FleetManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<VehicleWithUpcomingMaintenanceDto>> Handle(GetVehiclesWithUpcomingMaintenancesQuery request, CancellationToken cancellationToken)
        {
            DateTime currentDate = DateTime.UtcNow;
 
            var vehiclesWithUpcomingMaintenances = await _dbContext.Set<Vehicle>()
                .Where(x => EF.Functions.DateDiffDay(currentDate, x.NextInspectionDate) < 7)
                .Select(x => new VehicleWithUpcomingMaintenanceDto(
                    x.NextInspectionDate,
                    x.VehicleDetails.Vin,
                    x.VehicleDetails.LicensePlate,
                    x.VehicleDetails.Model
                ))
                .ToListAsync(cancellationToken);

            return vehiclesWithUpcomingMaintenances;
        }
    }
}
