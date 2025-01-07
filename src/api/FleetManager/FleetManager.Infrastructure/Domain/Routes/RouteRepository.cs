using FleetManager.Domain.Routes;
using FleetManager.Infrastructure.Data;

namespace FleetManager.Infrastructure.Domain.Routes
{
    internal class RouteRepository : IRouteRepository
    {
        private readonly FleetManagerDbContext _dbContext;

        public RouteRepository(FleetManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Route route, CancellationToken cancellationToken)
        {
            await _dbContext.Set<Route>().AddAsync(route, cancellationToken);
        }
    }
}
