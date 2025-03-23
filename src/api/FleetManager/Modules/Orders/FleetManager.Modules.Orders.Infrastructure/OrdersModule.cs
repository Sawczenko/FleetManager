using FleetManager.BuildingBlocks.Application;
using FleetManager.Modules.Orders.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Orders.Infrastructure;

public class OrdersModule : IModule
{
    public void InstallModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrdersContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("FleetManagerDatabase")));
    }

    public void UseModule()
    {
        throw new NotImplementedException();
    }
}