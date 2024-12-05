using FleetManager.Domain.VehicleUsages;
using Microsoft.AspNetCore.Identity;

namespace FleetManager.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<VehicleUsage> VehicleUsages { get; set; }
    }
}
