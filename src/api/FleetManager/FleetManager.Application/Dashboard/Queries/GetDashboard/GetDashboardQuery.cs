using FleetManager.Application.Vehicles.GetVehiclesWithUpcomingMaintenances;
using FleetManager.Application.Vehicles.GetVehiclesCountPerStatus;
using FleetManager.Application.Dashboard.Models;
using FleetManager.Application.Routes.GetTodaysRoutesCountPerStatus;
using MediatR;

namespace FleetManager.Application.Dashboard.Queries.GetDashboard;

public class GetDashboardQuery : IRequest<DashboardDto>
{
    
}

public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, DashboardDto>
{
    private readonly IMediator _mediator;

    public GetDashboardQueryHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var vehicleCountPerStatus = await _mediator.Send(new GetVehiclesCountPerStatusQuery(), cancellationToken);

        var vehiclesWithUpcomingMaintenance = await _mediator.Send(new GetVehiclesWithUpcomingMaintenancesQuery(), cancellationToken);

        var routesCountPerStatus = await _mediator.Send(new GetTodaysRouteCountPerStatusQuery(), cancellationToken);

        return new DashboardDto(vehicleCountPerStatus, vehiclesWithUpcomingMaintenance, routesCountPerStatus);
    }
}