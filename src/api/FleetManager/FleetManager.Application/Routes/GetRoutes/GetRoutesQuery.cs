using FleetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Routes;
using MediatR;
using System;

namespace FleetManager.Application.Routes.GetRoutes
{
    internal record RoutesQuery(
        string UserName,
        string StartLocationName,
        double StartLocationLatitude,
        double StartLocationLongitude,
        string EndLocationName,
        double EndLocationLatitude,
        double EndLocationLongitude,
        string Vehicle,
        DateTime ScheduledStartTime,
        DateTime? EndTime,
        int Status
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
                CONCAT(users.FirstName, ' ', users.LastName) AS UserName,
                startLocations.Name AS StartLocationName,
                startLocations.Latitude AS StartLocationLatitude,
                startLocations.Longitude AS StartLocationLongitude,
                endLocations.Name AS EndLocationName,
                endLocations.Latitude AS EndLocationLatitude,
                endLocations.Longitude AS EndLocationLongitude,
                CONCAT(vehicles.Model, ' ', vehicles.VIN) AS Vehicle,
                routes.ScheduledStartTime,
                routes.ActualEndTime AS EndTime,
                routes.Status
                FROM Routes routes
                JOIN Users users ON users.Id = routes.UserId
                JOIN Vehicles vehicles ON vehicles.Id = routes.VehicleId
                JOIN Locations startLocations ON startLocations.Id = routes.StartLocationId
                JOIN Locations endLocations ON endLocations.Id = routes.EndLocationId
                "
                );

            if (!string.IsNullOrWhiteSpace(routeFilter.UserName))
            {
                query = query.Where(x => x.UserName.Contains(routeFilter.UserName));
            }

            if (!string.IsNullOrWhiteSpace(routeFilter.Status))
            {
                RouteStatus status = (RouteStatus)Enum.Parse(typeof(RouteStatus), routeFilter.Status);

                query = query.Where(x => x.Status == (int)status);
            }

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
            x.UserName,
                    new LocationDto(
                        x.StartLocationName,
                        x.StartLocationLatitude,
                        x.StartLocationLongitude
                    ),
                    new LocationDto(
                        x.EndLocationName,
                        x.EndLocationLatitude,
                        x.EndLocationLongitude
                    ),
                    x.Vehicle,
                    x.ScheduledStartTime,
                    x.EndTime,
                    ((RouteStatus)x.Status).ToString()))
                .ToListAsync(cancellationToken);
        }
    }
}
