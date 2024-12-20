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

        public async Task<IEnumerable<VehicleDto>> Handle(GetVehiclesQuery query, CancellationToken cancellationToken)
        {
            return await _dbContext
                .Set<Vehicle>()
                .AsNoTracking()
                .Select(x => new VehicleDto(x.Id, x.VehicleDetails))
                .ToListAsync(cancellationToken);
        }
    }
}
