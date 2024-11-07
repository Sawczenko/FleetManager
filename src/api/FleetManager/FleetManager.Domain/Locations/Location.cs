namespace FleetManager.Domain.Locations
{
    public record Location
    {
        public Guid Id;
        public string Name;
        public decimal Latitude;
        public decimal Longitude;

        public Location(string name, decimal latitude, decimal longitude)
        {
            Id = Guid.NewGuid();
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Location(decimal latitude, decimal longitude)
            : this(null, latitude, longitude) { }

        public override string ToString()
        {
            return $"{Name ?? "Unnamed Location"} ({Latitude}, {Longitude})";
        }
    }

}
