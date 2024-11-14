using Microsoft.AspNetCore.Identity;

namespace GymBooker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ApplicationUserGymClass> GymClasses { get; set; } = [];
    }
}
