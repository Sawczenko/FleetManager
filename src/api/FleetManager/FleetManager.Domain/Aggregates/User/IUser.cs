namespace FleetManager.Domain.Aggregates.User
{
    public interface IUser
    {
        public string Id { get; set; } // Unikalny identyfikator użytkownika
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public List<Guid> VehicleUsageIds { get; set; }
    }
}
