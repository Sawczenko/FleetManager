using FleetManager.Domain.Itineraries;
using FleetManager.Domain.Routes;
using FluentAssertions;

namespace FleetManager.Tests.Unit.Itineraries
{
    public class ItineraryFactoryTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenRouteIdIsEmpty()
        {
            // Arrange
            var routes = new List<ItineraryRoute>
            {
                new ItineraryRoute(Guid.NewGuid(), Guid.Empty, 1)
            };
            var driverId = Guid.NewGuid();
            var vehicleId = Guid.NewGuid();
            var scheduledStartDate = DateTime.UtcNow.AddHours(1);
            var scheduledEndDate = scheduledStartDate.AddHours(2);

            // Act
            var result = ItineraryFactory.Create(routes, driverId, vehicleId, scheduledStartDate, scheduledEndDate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(ItineraryErrors.MissingRoute());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenDriverIdIsEmpty()
        {
            // Arrange
            var routes = new List<ItineraryRoute>
            {
                new ItineraryRoute(Guid.NewGuid(), Guid.NewGuid(), 1)
            };
            var driverId = Guid.Empty;
            var vehicleId = Guid.NewGuid();
            var scheduledStartDate = DateTime.UtcNow.AddHours(1);
            var scheduledEndDate = scheduledStartDate.AddHours(2);

            // Act
            var result = ItineraryFactory.Create(routes, driverId, vehicleId, scheduledStartDate, scheduledEndDate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(ItineraryErrors.MissingDriver());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenVehicleIdIsEmpty()
        {
            // Arrange
            var routes = new List<ItineraryRoute>
        {
            new ItineraryRoute(Guid.NewGuid(), Guid.NewGuid(), 1)
        };
            var driverId = Guid.NewGuid();
            var vehicleId = Guid.Empty;
            var scheduledStartDate = DateTime.UtcNow.AddHours(1);
            var scheduledEndDate = scheduledStartDate.AddHours(2);

            // Act
            var result = ItineraryFactory.Create(routes, driverId, vehicleId, scheduledStartDate, scheduledEndDate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(ItineraryErrors.MissingVehicle());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenScheduledStartDateIsInThePast()
        {
            // Arrange
            var routes = new List<ItineraryRoute>
        {
            new ItineraryRoute(Guid.NewGuid(), Guid.NewGuid(), 1)
        };
            var driverId = Guid.NewGuid();
            var vehicleId = Guid.NewGuid();
            var scheduledStartDate = DateTime.UtcNow.AddHours(-1);
            var scheduledEndDate = scheduledStartDate.AddHours(1);

            // Act
            var result = ItineraryFactory.Create(routes, driverId, vehicleId, scheduledStartDate, scheduledEndDate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(ItineraryErrors.ScheduledStartDateCannotBeInThePast());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenScheduledStartDateIsLaterThanScheduledEndDate()
        {
            // Arrange
            var routes = new List<ItineraryRoute>
        {
            new ItineraryRoute(Guid.NewGuid(), Guid.NewGuid(), 1)
        };
            var driverId = Guid.NewGuid();
            var vehicleId = Guid.NewGuid();
            var scheduledStartDate = DateTime.UtcNow.AddHours(2);
            var scheduledEndDate = scheduledStartDate.AddHours(-1);

            // Act
            var result = ItineraryFactory.Create(routes, driverId, vehicleId, scheduledStartDate, scheduledEndDate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(ItineraryErrors.ScheduledStartDateCannotBeLaterThanScheduledEndDate());
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenAllParametersAreValid()
        {
            // Arrange
            var routes = new List<ItineraryRoute>
            {
                new ItineraryRoute(Guid.NewGuid(), Guid.NewGuid(), 1)
            };
            var driverId = Guid.NewGuid();
            var vehicleId = Guid.NewGuid();
            var scheduledStartDate = DateTime.UtcNow.AddHours(1);
            var scheduledEndDate = scheduledStartDate.AddHours(2);

            // Act
            var result = ItineraryFactory.Create(routes, driverId, vehicleId, scheduledStartDate, scheduledEndDate);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }
    }
}
