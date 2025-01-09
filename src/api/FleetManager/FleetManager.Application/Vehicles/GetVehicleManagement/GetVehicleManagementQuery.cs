using FleetManager.Application.Vehicles.Shared;
using FleetManager.Domain.Vehicles.Models;
using FleetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace FleetManager.Application.Vehicles.GetVehicleManagement
{
    public record GetVehicleManagementQuery(Guid VehicleId) : IRequest<VehicleManagementDto>
    {
    }

    internal class GetVehicleManagementQueryHandler : IRequestHandler<GetVehicleManagementQuery, VehicleManagementDto?>
    {
        private readonly FleetManagerDbContext _dbContext;

        public GetVehicleManagementQueryHandler(FleetManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VehicleManagementDto?> Handle(GetVehicleManagementQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<Vehicle>()
                .Where(x => x.Id == request.VehicleId)
                .Select(x => new VehicleManagementDto(
                    x.Id,
                    x.VehicleDetails,
                    x.Status.ToString(),
                    x.NextInspectionDate,
                    x.Inspections.Select(y => new InspectionDto(y.Date, y.Description, y.Cost)).ToList(),
                    x.Repairs.Select(y => new RepairDto(y.Date, y.Description, y.Cost)).ToList()
                    ))
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
