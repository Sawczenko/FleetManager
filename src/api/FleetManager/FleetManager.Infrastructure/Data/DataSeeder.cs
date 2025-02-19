using FleetManager.Domain.Contractors;
using FleetManager.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;
using FleetManager.Domain.Vehicles.Models;
using FleetManager.Domain.Locations;
using FleetManager.Domain.Orders;
using Microsoft.AspNetCore.Identity;
using FleetManager.Domain.Vehicles;

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

            List<Location> locations = await AddLocations();

            List<Vehicle> vehicles = await AddVehicles(locations);

            List<Contractor> contractors = await AddContractors(locations);

            List<Order> orders = await AddOrders(locations, contractors);

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

        private async Task<List<Location>> AddLocations()
        {
            List<Location> locations = new List<Location>
            {
                new Location("Warsaw", 52.237049, 21.017532),
                new Location("Bydgoszcz", 53.123482, 18.008438),
                new Location("Gdańsk", 54.372158, 18.638306),
                new Location("Poznań", 52.409538, 16.931992)
            };

            await _dbContext.Set<Location>().AddRangeAsync(locations);

            return locations;
        }

        private async Task<List<Vehicle>> AddVehicles(List<Location> locations)
        {
            List<Vehicle> vehicles = new List<Vehicle>
            {
                VehicleFactory.Create(
                    "1HGCM82633A123456",
                    "ABC123",
                    "Toyota Corolla",
                    DateTime.UtcNow.AddYears(-1),
                    DateTime.UtcNow.AddYears(1),
                    locations[0]
                ).Value,
                VehicleFactory.Create(
                    "1HGCM82633A123465",
                    "ABC124",
                    "Toyota Avensis",
                    DateTime.UtcNow.AddYears(-1),
                    DateTime.UtcNow.AddDays(14),
                    locations[1],
                    VehicleStatus.InMainetance
                ).Value,
                VehicleFactory.Create(
                    "1HGCM82633A122365",
                    "ABC114",
                    "Toyota Avensis",
                    DateTime.UtcNow.AddYears(-1),
                    DateTime.UtcNow.AddDays(5),
                    locations[2],
                    VehicleStatus.InMainetance
                ).Value
            };

            await _dbContext.Set<Vehicle>()
                .AddRangeAsync(vehicles);

            var vehicle = vehicles[0];
            vehicle.AddInspection(new Inspection(vehicle.Id, DateTime.UtcNow, "Przegląd", 120));

            return vehicles;
        }


        private async Task<List<Contractor>> AddContractors(List<Location> locations)
        {
            List<Contractor> contractors = new List<Contractor>();

            contractors.Add(ContractorFactory.Create("Company A", locations[0].Id).Value);
            contractors.Add(ContractorFactory.Create("Company B", locations[1].Id).Value);

            await _dbContext.Set<Contractor>().AddRangeAsync(contractors);

            return contractors;
        }

        private async Task<List<Order>> AddOrders(List<Location> locations, List<Contractor> contractors)
        {
            List<Order> orders = new List<Order>();

            orders.Add(OrderFactory.Create(
                contractors[0].Id, 
                locations[0].Id, 
                locations[1].Id,
                DateTime.UtcNow.AddMinutes(10), 
                DateTime.UtcNow.AddDays(2)
                ).Value);

            for (int i = 1; i < 50; i++)
            {
                orders.Add(OrderFactory.Create(
                    contractors[1].Id,
                    locations[1].Id,
                    locations[2].Id,
                    DateTime.UtcNow.AddDays(3),
                    DateTime.UtcNow.AddDays(5)
                ).Value);
            }


            await _dbContext.Set<Order>().AddRangeAsync(orders);

            return orders;
        }
    }
}
