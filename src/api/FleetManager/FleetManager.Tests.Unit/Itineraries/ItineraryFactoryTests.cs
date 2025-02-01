using FleetManager.Domain.Itineraries;
using FleetManager.Domain.Itineraries.Checkpoints;
using FluentAssertions;

namespace FleetManager.Tests.Unit.Itineraries
{
    public class ItineraryFactoryTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenOrderRoutingsAreEmpty()
        {
            var result = ItineraryFactory.Create(new List<OrderRouting>(), Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2));

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(ItineraryErrors.MissingRoute());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenDriverIdIsEmpty()
        {
            var result = ItineraryFactory.Create(new List<OrderRouting> { new OrderRouting(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1) }, Guid.Empty, Guid.NewGuid(), DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2));

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(ItineraryErrors.MissingDriver());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenVehicleIdIsEmpty()
        {
            var result = ItineraryFactory.Create(new List<OrderRouting> { new OrderRouting(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1) }, Guid.NewGuid(), Guid.Empty, DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2));

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(ItineraryErrors.MissingVehicle());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenScheduledStartDateIsInThePast()
        {
            var result = ItineraryFactory.Create(new List<OrderRouting> { new OrderRouting(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1) }, Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddHours(-1), DateTime.UtcNow.AddHours(2));

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(ItineraryErrors.ScheduledStartDateCannotBeInThePast());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenScheduledStartDateIsAfterScheduledEndDate()
        {
            var result = ItineraryFactory.Create(new List<OrderRouting> { new OrderRouting(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1) }, Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddHours(2), DateTime.UtcNow.AddHours(1));

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(ItineraryErrors.ScheduledStartDateCannotBeLaterThanScheduledEndDate());
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenAllParametersAreValid()
        {
            var orderRoutings = new List<OrderRouting>
        {
            new OrderRouting(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 2),
            new OrderRouting(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1)
        };
            var driverId = Guid.NewGuid();
            var vehicleId = Guid.NewGuid();
            var startDate = DateTime.UtcNow.AddHours(1);
            var endDate = DateTime.UtcNow.AddHours(2);

            var result = ItineraryFactory.Create(orderRoutings, driverId, vehicleId, startDate, endDate);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Checkpoints.Should().HaveCount(4);
            result.Value.DriverId.Should().Be(driverId);
            result.Value.VehicleId.Should().Be(vehicleId);
            result.Value.StartDate.Should().Be(startDate);
            result.Value.EndDate.Should().Be(endDate);

            var orderedRoutings = orderRoutings.OrderBy(x => x.Sequence).ToList();
            for (int i = 0; i < orderedRoutings.Count; i++)
            {
                result.Value.Checkpoints[i * 2].LocationId.Should().Be(orderedRoutings[i].PickupLocationId);
                result.Value.Checkpoints[i * 2].Type.Should().Be(CheckpointType.Pickup);
                result.Value.Checkpoints[i * 2 + 1].LocationId.Should().Be(orderedRoutings[i].DeliveryLocationId);
                result.Value.Checkpoints[i * 2 + 1].Type.Should().Be(CheckpointType.Delivery);
            }
        }
    }

}