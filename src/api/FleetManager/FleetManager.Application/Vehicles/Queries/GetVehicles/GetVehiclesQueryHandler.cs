using FleetManager.Domain.Vehicles.Models;
using FleetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace FleetManager.Application.Vehicles.Queries.GetVehicles
{
    internal sealed class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, IEnumerable<Vehicle>>
    {
        private readonly FleetManagerDbContext _dbContext;

        public GetVehiclesQueryHandler(FleetManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Vehicle>> Handle(GetVehiclesQuery query, CancellationToken cancellationToken)
        {
            return await _dbContext
                .Vehicles
                .Include(x => x.Repairs)
                .Include(x => x.Inspections)
                .Include(x => x.CurrentLocation)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
