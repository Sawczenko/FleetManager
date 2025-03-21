using FleetManager.Modules.Orders.Domain.Orders;
using Shouldly;

namespace FleetManager.Modules.Orders.UnitTests.Orders
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
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBe(OrderErrors.MissingContractor());
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
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBe(OrderErrors.MissingOriginLocation());
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
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBe(OrderErrors.MissingDestinationLocation());
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
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBe(OrderErrors.InvalidPickupDate());
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
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBe(OrderErrors.InvalidDeliveryDate());
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
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBe(OrderErrors.PickupDateInThePast());
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
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBe(OrderErrors.DeliveryDateIsEarlierThanPickupDate());
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
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.ContractorId.ShouldBe(contractorId);
            result.Value.OriginId.ShouldBe(originId);
            result.Value.DestinationId.ShouldBe(destinationId);
            result.Value.PickupDate.ShouldBe(pickupDate);
            result.Value.DeliveryDate.ShouldBe(deliveryDate);
            result.Value.Status.ShouldBe(OrderStatus.Created);
        }
    }
}
