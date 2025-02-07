using FleetManager.Domain.Itineraries.Checkpoints;

namespace FleetManager.Domain.Itineraries;

public class Itinerary
{
    public Guid Id { get; private set; }

    public List<Checkpoint> Checkpoints { get; private set; }

    public Guid DriverId { get; private set; }

    public Guid VehicleId { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public ItineraryStatus Status { get; private set; }

    private Itinerary()
    {
        Checkpoints = new List<Checkpoint>();
    }

    internal Itinerary(List<Checkpoint> checkpoints, Guid driverId, Guid vehicleId, DateTime startDate, DateTime endDate)
    {
        Checkpoints = checkpoints.OrderBy(checkpoint => checkpoint.Sequence).ToList();
        DriverId = driverId;
        VehicleId = vehicleId;
        StartDate = startDate;
        EndDate = endDate;
        Status = ItineraryStatus.Planned;
    }

    public void Start()
    {
        Status = ItineraryStatus.InProgress;
        ActivateNextCheckpoint();
    }

    public void CompleteCheckpoint(Guid checkpointId)
    {
        Checkpoint checkpoint = Checkpoints.First(x => x.Id == checkpointId);

        checkpoint.Complete();

        if (AnyQueuedCheckpointRemaining())
        {
            ActivateNextCheckpoint();
        }
    }

    private bool AnyQueuedCheckpointRemaining()
    {
        return Checkpoints.Any(checkpoint => checkpoint.Status == CheckpointStatus.Queued);
    }

    private void ActivateNextCheckpoint()
    {
        Checkpoint nextCheckpoint = Checkpoints.First(checkpoint => checkpoint.Status == CheckpointStatus.Queued);

        nextCheckpoint.Activate();
    }
}