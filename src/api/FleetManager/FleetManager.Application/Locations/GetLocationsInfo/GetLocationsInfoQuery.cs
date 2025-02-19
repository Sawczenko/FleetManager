using FleetManager.Domain.Locations;
using FleetManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Application.Locations.GetLocationsInfo
{
    public record GetLocationsInfoQuery : IRequest<List<LocationInfo>>
    {
    }

    internal record GetLocationsInfoQueryHandler : IRequestHandler<GetLocationsInfoQuery, List<LocationInfo>>
    {
        private readonly FleetManagerDbContext _dbContext;

        public GetLocationsInfoQueryHandler(FleetManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LocationInfo>> Handle(GetLocationsInfoQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<Location>().Select(location => new LocationInfo(location.Id, location.Name))
                .ToListAsync(cancellationToken);
        }
    }
}
