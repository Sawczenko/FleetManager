using FleetManager.Domain.Routes;
using FleetManager.Infrastructure.Domain.Vehicles;
using Microsoft.Extensions.DependencyInjection;
using FleetManager.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using FleetManager.Infrastructure.Domain;
using FleetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FleetManager.Domain.SeedWork;
using FleetManager.Domain.Vehicles;
using FleetManager.Infrastructure.Domain.Routes;

namespace FleetManager.Infrastructure
{
    public static class Registration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FleetManagerDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("SqlDockerDevelopmentConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<FleetManagerDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddRepositories();
            services.AddDomainServices();
            services.AddJwtAuthentication(configuration);
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
        }

        private static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<VehicleService>();
            services.AddScoped<RouteService>();
        }
    }
}
