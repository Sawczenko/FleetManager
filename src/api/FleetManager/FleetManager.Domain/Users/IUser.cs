namespace FleetManager.Domain.Users
{
    public interface IUser
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public List<Guid> VehicleUsageIds { get; set; }
    }
}
