using FleetManager.Modules.Vehicles.Domain;
using FleetManager.Modules.Vehicles.Domain.Models;
using Shouldly;

namespace FleetManager.Modules.Vehicles.UnitTests
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
                Guid.NewGuid()
            );

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(VehicleErrors.MissingVehicleDetails(nameof(VehicleDetails.Vin).ToLower()));
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
                Guid.NewGuid()
            );

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(VehicleErrors.MissingVehicleDetails(nameof(licensePlate)));
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
                Guid.NewGuid()
            );

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(VehicleErrors.MissingVehicleDetails(nameof(VehicleDetails.Model).ToLower()));
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
                Guid.Empty,
                VehicleStatus.Available
            );

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(VehicleErrors.MissingInitialLocation);
        }

        [Fact]
        public void Create_WhenLastInspectionDateIsInTheFuture_ReturnsFailureWithFutureLastInspectionDateError()
        {
            // Arrange
            var futureLastInspectionDate = DateTime.UtcNow.AddYears(1);
            var result = VehicleFactory.Create(
                "1HGCM82633A123456",
                "ABC123",
                "ModelX",
                futureLastInspectionDate,
                DateTime.UtcNow.AddYears(2),
                Guid.NewGuid()
            );

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(VehicleErrors.FutureLastInspectionDate(futureLastInspectionDate, DateTime.UtcNow));
        }

        [Fact]
        public void Create_WhenNextInspectionDateIsInThePast_ReturnsFailureWithPastNextInspectionDateError()
        {
            // Arrange
            var pastNextInspectionDate = DateTime.UtcNow.AddYears(-2);
            var result = VehicleFactory.Create(
                "1HGCM82633A123456",
                "ABC123",
                "ModelX",
                DateTime.UtcNow.AddYears(-1),
                pastNextInspectionDate,
                Guid.NewGuid()
            );

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(VehicleErrors.PastNextInspectionDate(pastNextInspectionDate, DateTime.UtcNow));
        }

        [Fact]
        public void Create_WhenAllInputsAreValid_ReturnsSuccessWithValidVehicle()
        {
            // Arrange
            var currentLocation = Guid.NewGuid();
            var result = VehicleFactory.Create(
                "1HGCM82633A123456",
                "ABC123",
                "ModelX",
                DateTime.UtcNow.AddYears(-1),
                DateTime.UtcNow.AddYears(1),
                currentLocation
            );

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.ShouldBeOfType<Vehicle>();
            result.Value.VehicleDetails.Vin.ShouldBe("1HGCM82633A123456");
            result.Value.VehicleDetails.LicensePlate.ShouldBe("ABC123");
            result.Value.VehicleDetails.Model.ShouldBe("ModelX");
            result.Value.CurrentLocationId.ShouldBe(currentLocation);
        }
    }
}
