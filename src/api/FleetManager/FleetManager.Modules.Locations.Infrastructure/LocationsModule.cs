using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Locations.Infrastructure;

public static class LocationsModule
{
    public static void LoadLocationsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LocationsContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("SqlDockerDevelopmentConnection")));
    }
}