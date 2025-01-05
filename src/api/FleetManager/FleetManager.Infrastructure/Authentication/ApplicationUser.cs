using Microsoft.AspNetCore.Identity;
using FleetManager.Domain.Routes;

namespace FleetManager.Infrastructure.Authentication
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Route> Routes { get; set; }
    }
}
