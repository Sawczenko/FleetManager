using FleetManager.Modules.Itineraries.Infrastructure.Data;
using FleetManager.Modules.Itineraries.Application;
using FleetManager.BuildingBlocks.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Itineraries.Infrastructure;

public class ItinerariesModule : IModule
{
    public void InstallModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ItinerariesContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("FleetManagerDatabase")));
        
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(IItinerariesModule).Assembly));
    }

    public void UseModule()
    {
        throw new NotImplementedException();
    }

    public Task SeedDataAsync(IServiceProvider serviceProvider)
    {
        throw new NotImplementedException();
    }
}