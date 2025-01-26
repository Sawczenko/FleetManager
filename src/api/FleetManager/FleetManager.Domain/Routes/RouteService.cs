using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.SeedWork;

namespace FleetManager.Domain.Routes
{
    public class RouteService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RouteService(IRouteRepository routeRepository, IUnitOfWork unitOfWork)
        {
            _routeRepository = routeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> CreateNewRouteAsync(
            Guid startLocationId,
            Guid endLocationId,
            CancellationToken cancellationToken)
        {
            Result<Route> result = RouteFactory.Create(startLocationId, endLocationId);

            if (result.IsFailure)
            {
                return result;
            }

            await _routeRepository.AddAsync(result.Value, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
    }
}
