using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.Routes;
using MediatR;

namespace FleetManager.Application.Routes.CreateRoute
{
    public record CreateRouteCommand(CreateRouteRequest AddRouteRequest) : IRequest<Result>
    {
    }

    internal class CreateRouteCommandHandler : IRequestHandler<CreateRouteCommand, Result>
    {
        private readonly RouteService _routeService;

        public CreateRouteCommandHandler(RouteService routeService)
        {
            _routeService = routeService;
        }

        public async Task<Result> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
        {
            CreateRouteRequest addRouteRequest = request.AddRouteRequest;

            return await _routeService.CreateNewRouteAsync(
                Guid.Parse(addRouteRequest.StartLocationId), Guid.Parse(addRouteRequest.EndLocationId), cancellationToken);
        }
    }
}
