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
        Checkpoints = checkpoints;
        DriverId = driverId;
        VehicleId = vehicleId;
        StartDate = startDate;
        EndDate = endDate;
        Status = ItineraryStatus.Planned;
    }

    public void CheckpointCompleted(Guid checkpointId)
    {
        Checkpoint checkpoint = Checkpoints.First(x => x.Id == checkpointId);

        checkpoint.CheckpointCompleted();
    }
}