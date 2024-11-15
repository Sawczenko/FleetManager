namespace FleetManager.Domain.Locations
{
    public record Location
    {
        public Guid Id;
        public string Name;
        public decimal Latitude;
        public decimal Longitude;

        private Location()
        {
            
        }

        public Location(string name, decimal latitude, decimal longitude)
        {
            Id = Guid.NewGuid();
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Brak nazwy");
            }

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
