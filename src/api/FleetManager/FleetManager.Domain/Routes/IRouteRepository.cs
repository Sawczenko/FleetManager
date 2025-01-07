namespace FleetManager.Domain.Routes
{
    public interface IRouteRepository
    {
        public Task AddAsync(Route route, CancellationToken cancellationToken);
    }
}
