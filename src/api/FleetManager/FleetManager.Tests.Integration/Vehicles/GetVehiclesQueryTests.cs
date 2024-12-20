using FleetManager.Domain.Vehicles.Models;
using System.Net.Http.Json;
using FleetManager.Domain.Locations;
using FluentAssertions;
using FleetManager.Application.Vehicles.GetVehicles;

#nullable disable

namespace FleetManager.Tests.Integration.Vehicles
{
    public class GetVehiclesQueryTests : BaseIntegrationTest
    {
        private const string Endpoint = "/Vehicles";

        public GetVehiclesQueryTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetVehicles_ShouldReturnEmptyCollection_WhenThereAreNoVehicles()
        {
            #region Arrange
            #endregion

            #region Act

            var response = await HttpClient.GetAsync(Endpoint);


            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail("Error occurred during the request.");
            }

            var vehicles = await response.Content.ReadFromJsonAsync<IEnumerable<VehicleDto>>();

            #endregion

            #region Assert

            vehicles.Should().NotBeNull();
            vehicles.Should().BeEmpty();

            #endregion
        }

        [Fact]
        public async Task GetVehicles_ShouldReturnVehicles_WhenThereAreVehicles()
        {
            #region Arrange

            var expectedVehicles = new List<Vehicle>
            {
                new (new VehicleDetails("TESTVin_A", "TestA", "Ford F-150"),
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddYears(1),
                    new Location("TestLocationA", 10.00, 10.00)
                    , VehicleStatus.Available),
                new (new VehicleDetails("TESTVin_B", "TestB", "Ford F-150"),
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddYears(1),
                    new Location("TestLocationB", 11.00, 11.00),
                    VehicleStatus.InMainetance)
            };

            await DbContext.Vehicles.AddRangeAsync(expectedVehicles);

            await DbContext.SaveChangesAsync();
            #endregion

            #region Act

            var response = await HttpClient.GetAsync(Endpoint);


            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail("Error occurred during the request.");
            }

            //TODO: DTO
            var vehicles = await response.Content.ReadFromJsonAsync<IEnumerable<VehicleDto>>();

            #endregion

            #region Assert

            vehicles.Should().NotBeNullOrEmpty();
            vehicles.Count().Should().Be(expectedVehicles.Count);
            vehicles.Should().AllSatisfy(x =>
            {
                x.Id.Should().NotBe(Guid.Empty);
                x.Vin.Should().NotBeNull();
                x.LicensePlate.Should().NotBeNull();
                x.Model.Should().NotBeNull();
            });

            #endregion
        }
    }
}
