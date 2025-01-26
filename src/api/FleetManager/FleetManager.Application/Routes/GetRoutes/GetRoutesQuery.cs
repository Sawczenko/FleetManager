using FleetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace FleetManager.Application.Routes.GetRoutes
{
    internal record RoutesQuery(
        string StartLocationName,
        double StartLocationLatitude,
        double StartLocationLongitude,
        string EndLocationName,
        double EndLocationLatitude,
        double EndLocationLongitude
    ) { }

    public record GetRoutesQuery(RoutesFilterDto routesFilterDto) : IRequest<List<RouteDto>>
    {
    }

    internal class GetRoutesQueryHandler : IRequestHandler<GetRoutesQuery, List<RouteDto>>
    {
        private readonly FleetManagerDbContext _dbContext;

        public GetRoutesQueryHandler(FleetManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<RouteDto>> Handle(GetRoutesQuery request, CancellationToken cancellationToken)
        {
            RoutesFilterDto routeFilter = request.routesFilterDto;
            
            var query = _dbContext.Database
                .SqlQuery<RoutesQuery>(
                    @$"
                SELECT
                startLocations.Name AS StartLocationName,
                startLocations.Latitude AS StartLocationLatitude,
                startLocations.Longitude AS StartLocationLongitude,
                endLocations.Name AS EndLocationName,
                endLocations.Latitude AS EndLocationLatitude,
                endLocations.Longitude AS EndLocationLongitude
                FROM Routes routes
                JOIN Locations startLocations ON startLocations.Id = routes.StartLocationId
                JOIN Locations endLocations ON endLocations.Id = routes.EndLocationId
                "
                );

            if (!string.IsNullOrWhiteSpace(routeFilter.StartLocation))
            {
                query = query.Where(x => x.StartLocationName == routeFilter.StartLocation);
            }

            if (!string.IsNullOrWhiteSpace(routeFilter.EndLocation))
            {
                query = query.Where(x => x.EndLocationName == routeFilter.EndLocation);
            }

            return await query
            .Select(x => new RouteDto(
                    new LocationDto(
                        x.StartLocationName,
                        x.StartLocationLatitude,
                        x.StartLocationLongitude
                    ),
                    new LocationDto(
                        x.EndLocationName,
                        x.EndLocationLatitude,
                        x.EndLocationLongitude
                    )))
            .ToListAsync(cancellationToken);
        }
    }
}
