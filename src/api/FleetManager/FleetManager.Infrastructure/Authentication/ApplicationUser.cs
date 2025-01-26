using FleetManager.Domain.Itinerary;
using Microsoft.AspNetCore.Identity;

namespace FleetManager.Infrastructure.Authentication
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Itinerary> Itineraries { get; set; }
    }
}
