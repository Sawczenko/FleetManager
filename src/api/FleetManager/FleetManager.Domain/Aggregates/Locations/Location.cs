namespace FleetManager.Domain.Aggregates.Locations
{
    public record Location
    {
        public Guid Id;
        public string Name;
        public double Latitude;
        public double Longitude;

        private Location()
        {
            Name = string.Empty;
        }

        public Location(string name, double latitude, double longitude)
        {
            Id = Guid.NewGuid();
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return $"{Name} ({Latitude}, {Longitude})";
        }
    }

}
