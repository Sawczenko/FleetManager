using FleetManager.Domain.Vehicles.Models;
using FleetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace FleetManager.Application.Vehicles.GetVehicles
{
    internal sealed class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, IEnumerable<VehicleDto>>
    {
        private readonly FleetManagerDbContext _dbContext;

        public GetVehiclesQueryHandler(FleetManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<VehicleDto>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Vehicle> query = _dbContext.Set<Vehicle>()
                .AsNoTracking();

            VehiclesFilterDto vehiclesFilter = request.VehiclesFilter;

            if (!string.IsNullOrWhiteSpace(vehiclesFilter.Vin))
            {
                query = query.Where(x => x.VehicleDetails.Vin.Contains(vehiclesFilter.Vin));
            }

            if (!string.IsNullOrWhiteSpace(vehiclesFilter.LicensePlate))
            {
                query = query.Where(x => x.VehicleDetails.LicensePlate.Contains(vehiclesFilter.LicensePlate));
            }

            if (!string.IsNullOrWhiteSpace(vehiclesFilter.Model))
            {
                query = query.Where(x => x.VehicleDetails.Model.Contains(vehiclesFilter.Model));
            }

            return await query
                .Select(x => new VehicleDto(x.Id, x.VehicleDetails, x.Status.ToString()))
                .ToListAsync(cancellationToken);
        }
    }
}
