using FleetManager.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;
using FleetManager.Domain.Vehicles.Models;
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

            ApplicationUser applicationUser = await AddApplicationUser();

            List<Vehicle> vehicles = await AddVehicles();

            await AddRoutes(vehicles, applicationUser);

            await _dbContext.SaveChangesAsync();
        }

        private async Task<ApplicationUser> AddApplicationUser()
        {
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

            return user;
        }

        private async Task<List<Vehicle>> AddVehicles()
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

            var vehicle = vehicles[0];
            vehicle.AddInspection(new Inspection(vehicle.Id, DateTime.UtcNow, "Przegląd", 120));

            return vehicles;
        }

        private async Task AddRoutes(List<Vehicle> vehicles, ApplicationUser applicationUser)
        {
            var locations = new List<Location>
            {
                new Location("Test_A", 30.00, 32.00),
                new Location("Test_B", 35.00, 37.00),
                new Location("Test_C", 36.00, 40.00),
                new Location("Test_D", 37.00, 41.00),
                new Location("Test_E", 38.00, 42.00),
                new Location("Test_F", 39.00, 43.00)
            };

            await _dbContext.Set<Location>().AddRangeAsync(locations);

            await _dbContext.Set<Route>().AddAsync(RouteFactory
                .Create(locations[0].Id, locations[1].Id, applicationUser.Id, vehicles[0].Id).Value);

            await _dbContext.Set<Route>().AddAsync(RouteFactory
                .Create(locations[1].Id, locations[0].Id, applicationUser.Id, vehicles[1].Id).Value);

            await _dbContext.Set<Route>().AddAsync(RouteFactory
                .Create(locations[2].Id, locations[1].Id, applicationUser.Id, vehicles[2].Id).Value);

            //    await _dbContext.Set<Route>().AddAsync(RouteFactory.Create(locations[3].Id, locations[2].Id).Value);

            //    await _dbContext.Set<Route>().AddAsync(RouteFactory.Create(locations[3].Id, locations[1].Id).Value);

            //    await _dbContext.Set<Route>().AddAsync(RouteFactory.Create(locations[4].Id, locations[2].Id).Value);
            //}
        }

    }
}
