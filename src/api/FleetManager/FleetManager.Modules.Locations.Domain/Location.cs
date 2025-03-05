namespace FleetManager.Modules.Locations.Domain
{
    public class Location
    {
        public Guid Id;
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

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
