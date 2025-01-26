namespace FleetManager.Domain.Routes
{
    public class Route
    {
        public Guid Id { get; private set; }
        public Guid StartLocationId { get; private set; }
        public Guid EndLocationId { get; private set; }

        internal Route(Guid startLocationId, Guid endLocationId)
        {
            Id = Guid.NewGuid();
            StartLocationId = startLocationId;
            EndLocationId = endLocationId;
        }
    }

}
