using FleetManager.Domain.Itinerary;
using FluentAssertions;

namespace FleetManager.Tests.Unit.Itineraries
{
    public class ItineraryFactoryTests
    {
        [Fact]
        public void Create_WhenRouteIdIsEmpty_ReturnsFailureWithMissingRouteError()
        {
            // Arrange
            var result = ItineraryFactory.Create(
                Guid.Empty,
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2)
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ItineraryErrors.MissingRoute());
        }

        [Fact]
        public void Create_WhenUserIdIsEmpty_ReturnsFailureWithMissingDriverError()
        {
            // Arrange
            var result = ItineraryFactory.Create(
                Guid.NewGuid(),
                Guid.Empty,
                Guid.NewGuid(),
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2)
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ItineraryErrors.MissingDriver());
        }

        [Fact]
        public void Create_WhenVehicleIdIsEmpty_ReturnsFailureWithMissingVehicleError()
        {
            // Arrange
            var result = ItineraryFactory.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.Empty,
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2)
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ItineraryErrors.MissingVehicle());
        }

        [Fact]
        public void Create_WhenScheduledStartDateIsInThePast_ReturnsFailureWithScheduledStartDateCannotBeInThePastError()
        {
            // Arrange
            var result = ItineraryFactory.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.UtcNow.AddHours(-1),
                DateTime.UtcNow.AddHours(1)
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ItineraryErrors.ScheduledStartDateCannotBeInThePast());
        }

        [Fact]
        public void Create_WhenScheduledStartDateIsLaterThanScheduledEndDate_ReturnsFailureWithScheduledStartDateCannotBeLaterThanScheduledEndDateError()
        {
            // Arrange
            var result = ItineraryFactory.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.UtcNow.AddHours(2),
                DateTime.UtcNow.AddHours(1)
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ItineraryErrors.ScheduledStartDateCannotBeLaterThanScheduledEndDate());
        }

        [Fact]
        public void Create_WhenAllParametersAreValid_ReturnsSuccessWithValidItinerary()
        {
            // Arrange
            var routeId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var vehicleId = Guid.NewGuid();
            var scheduledStartDate = DateTime.UtcNow.AddHours(1);
            var scheduledEndDate = DateTime.UtcNow.AddHours(2);

            // Act
            var result = ItineraryFactory.Create(routeId, userId, vehicleId, scheduledStartDate, scheduledEndDate);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeOfType<Domain.Itinerary.Itinerary>();
            result.Value.RouteId.Should().Be(routeId);
            result.Value.DriverId.Should().Be(userId);
            result.Value.VehicleId.Should().Be(vehicleId);
            result.Value.ScheduledStartDate.Should().Be(scheduledStartDate);
            result.Value.ScheduledEndDate.Should().Be(scheduledEndDate);
            result.Value.Status.Should().Be(ItineraryStatus.Planned);
        }
    }
}
