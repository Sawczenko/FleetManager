using FleetManager.Modules.Itineraries.Domain;
using FleetManager.Modules.Itineraries.Domain.Checkpoints;
using Shouldly;

namespace FleetManager.Modules.Itineraries.UnitTests
{
    public class ItineraryFactoryTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenOrderRoutingsAreEmpty()
        {
            var result = ItineraryFactory.Create(new List<OrderRouting>(), Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2));

            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBe(ItineraryErrors.MissingRoute());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenDriverIdIsEmpty()
        {
            var result = ItineraryFactory.Create(
                new List<OrderRouting> { new OrderRouting(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1) },
                Guid.Empty,
                Guid.NewGuid(),
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2)
            );

            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(ItineraryErrors.MissingDriver());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenVehicleIdIsEmpty()
        {
            var result = ItineraryFactory.Create(
                new List<OrderRouting> { new OrderRouting(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1) },
                Guid.NewGuid(),
                Guid.Empty,
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2)
            );

            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(ItineraryErrors.MissingVehicle());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenScheduledStartDateIsInThePast()
        {
            var pastStartDate = DateTime.UtcNow.AddHours(-1);
            var result = ItineraryFactory.Create(
                new List<OrderRouting> { new OrderRouting(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1) },
                Guid.NewGuid(),
                Guid.NewGuid(),
                pastStartDate,
                DateTime.UtcNow.AddHours(2)
            );

            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(ItineraryErrors.ScheduledStartDateCannotBeInThePast());
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenScheduledStartDateIsAfterScheduledEndDate()
        {
            var startDate = DateTime.UtcNow.AddHours(2);
            var endDate = DateTime.UtcNow.AddHours(1);
            var result = ItineraryFactory.Create(
                new List<OrderRouting> { new OrderRouting(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1) },
                Guid.NewGuid(),
                Guid.NewGuid(),
                startDate,
                endDate
            );

            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(ItineraryErrors.ScheduledStartDateCannotBeLaterThanScheduledEndDate());
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

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Checkpoints.Count.ShouldBe(4);
            result.Value.DriverId.ShouldBe(driverId);
            result.Value.VehicleId.ShouldBe(vehicleId);
            result.Value.StartDate.ShouldBe(startDate);
            result.Value.EndDate.ShouldBe(endDate);

            var orderedRoutings = orderRoutings.OrderBy(x => x.Sequence).ToList();
            for (int i = 0; i < orderedRoutings.Count; i++)
            {
                result.Value.Checkpoints[i * 2].LocationId.ShouldBe(orderedRoutings[i].PickupLocationId);
                result.Value.Checkpoints[i * 2].Type.ShouldBe(CheckpointType.Pickup);
                result.Value.Checkpoints[i * 2 + 1].LocationId.ShouldBe(orderedRoutings[i].DeliveryLocationId);
                result.Value.Checkpoints[i * 2 + 1].Type.ShouldBe(CheckpointType.Delivery);
            }
        }
    }
}
