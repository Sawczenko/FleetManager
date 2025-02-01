namespace FleetManager.Domain.Itineraries.Checkpoints
{
    public class Checkpoint
    {
        public Guid Id { get; set; }

        public Guid ItineraryId { get; private set; }

        public Guid OrderId { get; private set; }

        public Guid LocationId { get; private set; }

        public CheckpointStatus Status { get; private set; }

        public CheckpointType Type { get; private set; }

        public Checkpoint(Guid orderId, Guid locationId, CheckpointType type)
        {
            OrderId = orderId;
            LocationId = locationId;
            Status = CheckpointStatus.Queued;
            Type = type;
        }

        public void CheckpointCompleted()
        {
            Status = CheckpointStatus.Completed;
            //event
        }
    }
}
