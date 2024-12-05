using Microsoft.Extensions.DependencyInjection;
using FleetManager.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;
using System.Data.Common;
using Respawn;

namespace FleetManager.Tests.Integration
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
            .WithPassword("Strong_password_123!")
            .Build();

        private DbConnection _dbConnection = default!;
        private Respawner _respawner = default!;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureTestServices(services =>
            {
                var descriptor = services
                    .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<FleetManagerDbContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<FleetManagerDbContext>(options =>
                    options.UseSqlServer(_dbContainer.GetConnectionString()));
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            _dbConnection = new SqlConnection(_dbContainer.GetConnectionString());

            await InitializeRespawner();
        }

        private async Task InitializeRespawner()
        {
            await _dbConnection.OpenAsync();

            var options = new DbContextOptionsBuilder<FleetManagerDbContext>().UseSqlServer(_dbContainer.GetConnectionString()).Options;

            var dbContext = new FleetManagerDbContext(options);
            await dbContext.Database.EnsureCreatedAsync();

            _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
            {

                SchemasToInclude = new[]
                    {
                    "dbo"
                },
                DbAdapter = DbAdapter.SqlServer,
                CommandTimeout = 60
            });
        }

        public Task ResetDatabaseAsync()
        {
            return _respawner.ResetAsync(_dbConnection);
        }

        public new Task DisposeAsync()
        {
            return _dbContainer.StopAsync();
        }
    }
}
