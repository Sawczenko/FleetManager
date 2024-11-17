using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FleetManager.Infrastructure.Domain;
using FleetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.SeedWork;
using FleetManager.Infrastructure.Domain.Vehicles;
using FleetManager.Domain.Aggregates.Vehicles;

namespace FleetManager.Infrastructure
{
    public static class Registration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FleetManagerDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddRepositories();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IVehicleRepository, VehicleRepository>();
        }
    }
}
