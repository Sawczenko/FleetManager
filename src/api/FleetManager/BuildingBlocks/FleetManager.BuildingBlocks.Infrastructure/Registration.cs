using FleetManager.BuildingBlocks.Application.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace FleetManager.BuildingBlocks.Infrastructure;

public static class Registration
{
    public static void AddInfrastructureBuildingBlocks(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("FleetManagerDatabase");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ApplicationException("Missing connection string in appsettings.json");
        }
        
        services.AddScoped<ISqlConnectionFactory>(x => new SqlConnectionFactory(connectionString));
    }
}