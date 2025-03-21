using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Modules.Orders.Infrastructure;

public static class OrdersModule
{
    public static void LoadOrdersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrdersContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("SqlDockerDevelopmentConnection")));
    }
}