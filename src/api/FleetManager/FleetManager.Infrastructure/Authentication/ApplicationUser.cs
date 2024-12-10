using FleetManager.Domain.VehicleUsages;
using Microsoft.AspNetCore.Identity;

namespace FleetManager.Infrastructure.Authentication
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<VehicleUsage> VehicleUsages { get; set; }
    }
}
