using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManager.Modules.Contractors.Infrastructure;

public static class ContractorsModule
{
    public static void LoadContractorsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ContractorsContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("SqlDockerDevelopmentConnection")));
    }
}