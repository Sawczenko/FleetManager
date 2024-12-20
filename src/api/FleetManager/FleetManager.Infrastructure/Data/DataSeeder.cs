using FleetManager.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;
using FleetManager.Domain.Vehicles.Models;
using FleetManager.Domain.VehicleUsages;
using FleetManager.Domain.Locations;
using Microsoft.AspNetCore.Identity;
using FleetManager.Domain.Routes;

#nullable disable

namespace FleetManager.Infrastructure.Data
{
    public class DataSeeder(IServiceProvider serviceProvider)
    {
        private FleetManagerDbContext _dbContext;
        private PasswordHasher<ApplicationUser> _passwordHasher;
        private UserManager<ApplicationUser> _userManager;

        public async Task SeedDataAsync()
        {
            using var scope = serviceProvider.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<FleetManagerDbContext>();
            _passwordHasher = new PasswordHasher<ApplicationUser>();
            _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedData();
        }

        private async Task SeedData()
        {
            await _dbContext.Database.EnsureDeletedAsync();

            await _dbContext.Database.EnsureCreatedAsync();

            await AddVehicles();

            await AddRoutes();

            await AddVehicleUsages();

            await _dbContext.SaveChangesAsync();
        }

        private async Task AddVehicles()
        {

            List<Vehicle> vehicles = new List<Vehicle>
            {
                VehicleFactory.Create(
                    "1HGCM82633A123456",
                    "ABC123",
                    "Toyota Corolla",
                    DateTime.UtcNow.AddYears(-1),
                    DateTime.UtcNow.AddYears(1),
                    new Location("Warsaw", 54.23, 54.45)
                ).Value,
                VehicleFactory.Create(
                    "1HGCM82633A123465",
                    "ABC124",
                    "Toyota Avensis",
                    DateTime.UtcNow.AddYears(-1),
                    DateTime.UtcNow.AddDays(14),
                    new Location("Warsaw", 54.21, 54.46),
                    VehicleStatus.InMainetance
                ).Value,
                VehicleFactory.Create(
                    "1HGCM82633A122365",
                    "ABC114",
                    "Toyota Avensis",
                    DateTime.UtcNow.AddYears(-1),
                    DateTime.UtcNow.AddDays(5),
                    new Location("Warsaw", 54.21, 54.46),
                    VehicleStatus.InMainetance
                ).Value
            };

            await _dbContext.Set<Vehicle>()
                .AddRangeAsync(vehicles);
        }

        private async Task AddRoutes()
        {
            var locations = new List<Location>
            {
                new Location("First", 30.00, 32.00),
                new Location("Second", 35.00, 37.00)
            };

            await _dbContext.Set<Location>().AddRangeAsync(locations);

            await _dbContext.Set<Route>().AddAsync(RouteFactory.Create(locations[0].Id, locations[1].Id).Value);

            await _dbContext.Set<Route>().AddAsync(RouteFactory.Create(locations[1].Id, locations[0].Id).Value);
        }


        private async Task AddVehicleUsages()
        {
            List<Vehicle> vehicles = new List<Vehicle>
            {
                VehicleFactory.Create(
                    "1HGCM75633A123456",
                    "ABC563",
                    "Toyota Yaris",
                    DateTime.UtcNow.AddYears(-1),
                    DateTime.UtcNow.AddYears(1),
                    new Location("Warsaw", 54.23, 54.45)
                ).Value,
                VehicleFactory.Create(
                    "1HGCM84533A123465",
                    "ABC143",
                    "Toyota Avensis",
                    DateTime.UtcNow.AddYears(-1),
                    DateTime.UtcNow.AddDays(14),
                    new Location("Warsaw", 54.21, 54.46),
                    VehicleStatus.InMainetance
                ).Value
            };

            await _dbContext.Set<Vehicle>().AddRangeAsync(vehicles);

            var locations = new List<Location>
            {
                new Location("First", 36.00, 37.00),
                new Location("Second", 33.00, 31.00)
            };

            await _dbContext.Set<Location>().AddRangeAsync(locations);

            var routes = new List<Route>
            {
                RouteFactory.Create(locations[0].Id, locations[1].Id).Value,
                RouteFactory.Create(locations[1].Id, locations[0].Id).Value
            };

            routes[0].CompleteRoute();

            await _dbContext.Set<Route>().AddRangeAsync(routes);


            string userEmail = "seededUser1@test.com";
            string userPassword = "TestPassword123!";
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = userEmail,
                FirstName = "Test",
                LastName = "User",
                NormalizedEmail = userEmail.ToUpper(),
                UserName = "testuser@example.com",
                PasswordHash = _passwordHasher.HashPassword(default, userPassword)
            };

            await _userManager.CreateAsync(user);

            var vehicleUsages = new List<VehicleUsage>
            {
                VehicleUsageFactory.Create(vehicles[0].Id, routes[0].Id, user.Id, routes[0].ScheduledStartTime).Value
            };

            vehicleUsages[0].EndUsage(DateTime.UtcNow);

            await _dbContext.Set<VehicleUsage>().AddRangeAsync(vehicleUsages);
        }
    }
}
