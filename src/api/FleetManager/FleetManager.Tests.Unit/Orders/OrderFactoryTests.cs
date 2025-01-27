using FleetManager.Domain.Orders;
using FluentAssertions;

namespace FleetManager.Tests.Unit.Orders
{
    public class OrderFactoryTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenContractorIdIsEmpty()
        {
            // Arrange
            Guid contractorId = Guid.Empty;
            Guid originId = Guid.NewGuid();
            Guid destinationId = Guid.NewGuid();
            DateTime pickupDate = DateTime.UtcNow.AddDays(1);
            DateTime deliveryDate = DateTime.UtcNow.AddDays(2);

            // Act
            var result = OrderFactory.Create(contractorId, originId, destinationId, pickupDate, deliveryDate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(OrderErrors.MissingContractor());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenOriginIdIsEmpty()
        {
            // Arrange
            Guid contractorId = Guid.NewGuid();
            Guid originId = Guid.Empty;
            Guid destinationId = Guid.NewGuid();
            DateTime pickupDate = DateTime.UtcNow.AddDays(1);
            DateTime deliveryDate = DateTime.UtcNow.AddDays(2);

            // Act
            var result = OrderFactory.Create(contractorId, originId, destinationId, pickupDate, deliveryDate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(OrderErrors.MissingOriginLocation());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenDestinationIdIsEmpty()
        {
            // Arrange
            Guid contractorId = Guid.NewGuid();
            Guid originId = Guid.NewGuid();
            Guid destinationId = Guid.Empty;
            DateTime pickupDate = DateTime.UtcNow.AddDays(1);
            DateTime deliveryDate = DateTime.UtcNow.AddDays(2);

            // Act
            var result = OrderFactory.Create(contractorId, originId, destinationId, pickupDate, deliveryDate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(OrderErrors.MissingDestinationLocation());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenPickupDateIsDefault()
        {
            // Arrange
            Guid contractorId = Guid.NewGuid();
            Guid originId = Guid.NewGuid();
            Guid destinationId = Guid.NewGuid();
            DateTime pickupDate = default;
            DateTime deliveryDate = DateTime.UtcNow.AddDays(2);

            // Act
            var result = OrderFactory.Create(contractorId, originId, destinationId, pickupDate, deliveryDate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(OrderErrors.InvalidPickupDate());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenDeliveryDateIsDefault()
        {
            // Arrange
            Guid contractorId = Guid.NewGuid();
            Guid originId = Guid.NewGuid();
            Guid destinationId = Guid.NewGuid();
            DateTime pickupDate = DateTime.UtcNow.AddDays(1);
            DateTime deliveryDate = default;

            // Act
            var result = OrderFactory.Create(contractorId, originId, destinationId, pickupDate, deliveryDate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(OrderErrors.InvalidDeliveryDate());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenPickupDateIsInThePast()
        {
            // Arrange
            Guid contractorId = Guid.NewGuid();
            Guid originId = Guid.NewGuid();
            Guid destinationId = Guid.NewGuid();
            DateTime pickupDate = DateTime.UtcNow.AddDays(-1);
            DateTime deliveryDate = DateTime.UtcNow.AddDays(1);

            // Act
            var result = OrderFactory.Create(contractorId, originId, destinationId, pickupDate, deliveryDate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(OrderErrors.PickupDateInThePast());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenDeliveryDateIsEarlierThanPickupDate()
        {
            // Arrange
            Guid contractorId = Guid.NewGuid();
            Guid originId = Guid.NewGuid();
            Guid destinationId = Guid.NewGuid();
            DateTime pickupDate = DateTime.UtcNow.AddDays(2);
            DateTime deliveryDate = DateTime.UtcNow.AddDays(1);

            // Act
            var result = OrderFactory.Create(contractorId, originId, destinationId, pickupDate, deliveryDate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(OrderErrors.DeliveryDateIsEarlierThanPickupDate());
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenAllInputsAreValid()
        {
            // Arrange
            Guid contractorId = Guid.NewGuid();
            Guid originId = Guid.NewGuid();
            Guid destinationId = Guid.NewGuid();
            DateTime pickupDate = DateTime.UtcNow.AddDays(1);
            DateTime deliveryDate = DateTime.UtcNow.AddDays(2);

            // Act
            var result = OrderFactory.Create(contractorId, originId, destinationId, pickupDate, deliveryDate);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.ContractorId.Should().Be(contractorId);
            result.Value.OriginId.Should().Be(originId);
            result.Value.DestinationId.Should().Be(destinationId);
            result.Value.PickupDate.Should().Be(pickupDate);
            result.Value.DeliveryDate.Should().Be(deliveryDate);
            result.Value.Status.Should().Be(OrderStatus.Created);
        }
    }
}
