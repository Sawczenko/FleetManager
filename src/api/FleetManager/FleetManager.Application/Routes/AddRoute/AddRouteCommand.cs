using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.Routes;
using MediatR;

namespace FleetManager.Application.Routes.AddRoute
{
    public record AddRouteCommand(AddRouteRequest AddRouteRequest) : IRequest<Result>
    {
    }

    internal class AddRouteCommandHandler : IRequestHandler<AddRouteCommand, Result>
    {
        private readonly RouteService _routeService;

        public AddRouteCommandHandler(RouteService routeService)
        {
            _routeService = routeService;
        }

        public async Task<Result> Handle(AddRouteCommand request, CancellationToken cancellationToken)
        {
            AddRouteRequest addRouteRequest = request.AddRouteRequest;

            return await _routeService.AddNewRouteAsync(
                Guid.Parse(addRouteRequest.StartLocationId), Guid.Parse(addRouteRequest.EndLocationId), cancellationToken);
        }
    }
}
