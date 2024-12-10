using FleetManager.Application.Account;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManager.Application
{
    public static class Registration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(Registration).Assembly));

            services.AddScoped<AccountService>();
        }
    }
}
