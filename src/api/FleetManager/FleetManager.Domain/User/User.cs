namespace FleetManager.Domain.User
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public List<Guid> VehicleUsageIds { get; private set; } = new List<Guid>();

        private User() { }

        internal User(string firstName, string lastName, string email)
        {
            Id = Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException();
            }

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            VehicleUsageIds = new List<Guid>();
        }

        public void AssignVehicleUsage(Guid vehicleUsageId)
        {
            if (!VehicleUsageIds.Contains(vehicleUsageId))
                VehicleUsageIds.Add(vehicleUsageId);
        }

        public void RemoveVehicleUsage(Guid vehicleUsageId)
        {
            VehicleUsageIds.Remove(vehicleUsageId);
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }

}
