using Microsoft.Extensions.DependencyInjection;
using FleetManager.Infrastructure.Data;

namespace FleetManager.Tests.Integration
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IAsyncLifetime
    {
        private readonly Func<Task> _resetDatabaseTask;
        private readonly IServiceScope _scope;
        protected readonly FleetManagerDbContext DbContext;
        protected HttpClient HttpClient;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            DbContext = _scope.ServiceProvider.GetRequiredService<FleetManagerDbContext>();
            _resetDatabaseTask = factory.ResetDatabaseAsync;
            HttpClient = factory.CreateClient();
        }

        protected FleetManagerDbContext GetCatalogDbContext()
        {
            return DbContext;
        }

        public virtual Task InitializeAsync()
        {
            return _resetDatabaseTask();
        }

        public async Task DisposeAsync()
        {
            await _resetDatabaseTask();
            _scope?.Dispose();
            DbContext?.Dispose();
        }
    }
}
