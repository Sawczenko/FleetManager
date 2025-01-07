using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FleetManager.Domain.SeedWork;
using FleetManager.Domain.SeedWork.Results;

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

        public async Task<Result> AddNewRouteAsync(Guid userId,
            Guid vehicleId,
            DateTime scheduledStartTime,
            Guid startLocationId,
            Guid endLocationId,
            CancellationToken cancellationToken)
        {
            Result<Route> result = RouteFactory.Create(startLocationId, endLocationId, userId, vehicleId);

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
