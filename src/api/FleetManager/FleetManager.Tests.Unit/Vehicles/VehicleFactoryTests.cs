using FleetManager.Domain.Locations;
using FleetManager.Domain.Vehicles.Models;
using FleetManager.Domain.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace FleetManager.Tests.Unit.Vehicles
{
    public class VehicleFactoryTests
    {
        [Fact]
        public void Create_WhenVinIsNullOrWhitespace_ReturnsFailureWithMissingVehicleDetailsError()
        {
            // Arrange
            var result = VehicleFactory.Create(
                "",
                "ABC123",
                "ModelX",
                DateTime.UtcNow.AddYears(-1),
                DateTime.UtcNow.AddYears(1),
                new Location("New",  50.0, 20.0)
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(VehicleErrors.MissingVehicleDetails(nameof(VehicleDetails.Vin).ToLower()));
        }

        [Fact]
        public void Create_WhenLicensePlateIsNullOrWhitespace_ReturnsFailureWithMissingVehicleDetailsError()
        {
            // Arrange
            string licensePlate = string.Empty;

            var result = VehicleFactory.Create(
                "1HGCM82633A123456",
                licensePlate,
                "ModelX",
                DateTime.UtcNow.AddYears(-1),
                DateTime.UtcNow.AddYears(1),
                new Location( "Location1", 50.0, 20.0)
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(VehicleErrors.MissingVehicleDetails(nameof(licensePlate)));
        }

        [Fact]
        public void Create_WhenModelIsNullOrWhitespace_ReturnsFailureWithMissingVehicleDetailsError()
        {
            // Arrange
            var result = VehicleFactory.Create(
                "1HGCM82633A123456",
                "ABC123",
                "",
                DateTime.UtcNow.AddYears(-1),
                DateTime.UtcNow.AddYears(1),
                new Location("Location1", 50.0, 20.0)
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(VehicleErrors.MissingVehicleDetails(nameof(VehicleDetails.Model).ToLower()));
        }

        [Fact]
        public void Create_WhenCurrentLocationIsNull_ReturnsFailureWithMissingInitialLocationError()
        {
            // Arrange
            var result = VehicleFactory.Create(
                "1HGCM82633A123456",
                "ABC123",
                "ModelX",
                DateTime.UtcNow.AddYears(-1),
                DateTime.UtcNow.AddYears(1),
                null,
                VehicleStatus.Available
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(VehicleErrors.MissingInitialLocation);
        }

        [Fact]
        public void Create_WhenLastInspectionDateIsInTheFuture_ReturnsFailureWithFutureLastInspectionDateError()
        {
            // Arrange
            var result = VehicleFactory.Create(
                "1HGCM82633A123456",
                "ABC123",
                "ModelX",
                DateTime.UtcNow.AddYears(1),
                DateTime.UtcNow.AddYears(2),
                new Location("Location1", 50.0, 20.0)
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(VehicleErrors.FutureLastInspectionDate(DateTime.UtcNow.AddYears(1), DateTime.UtcNow));
        }

        [Fact]
        public void Create_WhenNextInspectionDateIsInThePast_ReturnsFailureWithPastNextInspectionDateError()
        {
            // Arrange
            var result = VehicleFactory.Create(
                "1HGCM82633A123456",
                "ABC123",
                "ModelX",
                DateTime.UtcNow.AddYears(-1),
                DateTime.UtcNow.AddYears(-2),
                new Location("Location1", 50.0, 20.0)
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(VehicleErrors.PastNextInspectionDate(DateTime.UtcNow.AddYears(-2), DateTime.UtcNow));
        }

        [Fact]
        public void Create_WhenAllInputsAreValid_ReturnsSuccessWithValidVehicle()
        {
            // Arrange
            var currentLocation = new Location("Location1", 50.0, 20.0);
            var result = VehicleFactory.Create(
                "1HGCM82633A123456",
                "ABC123",
                "ModelX",
                DateTime.UtcNow.AddYears(-1),
                DateTime.UtcNow.AddYears(1),
                currentLocation
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeOfType<Vehicle>();
            result.Value.VehicleDetails.Vin.Should().Be("1HGCM82633A123456");
            result.Value.VehicleDetails.LicensePlate.Should().Be("ABC123");
            result.Value.VehicleDetails.Model.Should().Be("ModelX");
            result.Value.CurrentLocation.Should().Be(currentLocation);
        }
    }
}
