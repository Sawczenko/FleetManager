using FleetManager.Domain.Routes;
using FluentAssertions;

namespace FleetManager.Tests.Unit.Routes
{
    public class RouteFactoryTests
    {
        [Fact]
        public void Create_WhenStartLocationIdIsEmpty_ReturnsFailureWithMissingLocationError()
        {
            // Arrange
            var result = RouteFactory.Create(
                Guid.Empty,
                Guid.NewGuid()
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(RouteErrors.MissingLocation());
        }

        [Fact]
        public void Create_WhenEndLocationIdIsEmpty_ReturnsFailureWithMissingLocationError()
        {
            // Arrange
            var result = RouteFactory.Create(
                Guid.NewGuid(),
                Guid.Empty
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(RouteErrors.MissingLocation());
        }

        [Fact]
        public void Create_WhenBothStartAndEndLocationIdsAreEmpty_ReturnsFailureWithMissingLocationError()
        {
            // Arrange
            var result = RouteFactory.Create(
                Guid.Empty,
                Guid.Empty
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(RouteErrors.MissingLocation());
        }

        [Fact]
        public void Create_WhenBothStartAndEndLocationIdsAreValid_ReturnsSuccessWithValidRoute()
        {
            // Arrange
            var startLocationId = Guid.NewGuid();
            var endLocationId = Guid.NewGuid();

            // Act
            var result = RouteFactory.Create(startLocationId, endLocationId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeOfType<Route>();
            result.Value.StartLocationId.Should().Be(startLocationId);
            result.Value.EndLocationId.Should().Be(endLocationId);
        }
    }
}
