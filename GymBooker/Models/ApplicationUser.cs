﻿using Microsoft.AspNetCore.Identity;

namespace GymBooker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public DateTime TimeOfRegistration { get; set; }
        public ICollection<ApplicationUserGymClass> GymClasses { get; set; } = [];
    }
}
