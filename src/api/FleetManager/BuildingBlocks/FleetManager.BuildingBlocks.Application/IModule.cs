using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManager.BuildingBlocks.Application;

public interface IModule
{
    public void InstallModule(IServiceCollection services, IConfiguration configuration);

    public void UseModule();
}