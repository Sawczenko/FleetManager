using Microsoft.Extensions.DependencyInjection;
using FleetManager.Domain.Aggregates.Vehicles;

namespace FleetManager.Domain
{
    public static class Registration
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddScoped<VehicleService>();
        }
    }
}
