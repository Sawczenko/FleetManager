using Microsoft.Extensions.DependencyInjection;
using FleetManager.Domain.Aggregates.Locations;
using FleetManager.Domain.Aggregates.Vehicles;

#nullable disable

namespace FleetManager.Infrastructure.Data
{
    public class DataSeeder(IServiceProvider serviceProvider)
    {
        public async Task SeedDataAsync()
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<FleetManagerDbContext>();

            await SeedDataAsync(dbContext);
        }

        private async Task SeedDataAsync(FleetManagerDbContext dbContext)
        {
            await dbContext.Database.EnsureDeletedAsync();

            await dbContext.Database.EnsureCreatedAsync();

            await dbContext.Set<Vehicle>()
                .AddAsync(VehicleFactory.Create(
                    "1HGCM82633A123456", 
                    "ABC123", 
                    "Toyota Corolla",
                    DateTime.UtcNow.AddYears(-1),
                    DateTime.UtcNow.AddYears(1),
                    new Location("Warsaw", 54.23,54.45)
                ).Value);

            await dbContext.SaveChangesAsync();
        }
    }
}
