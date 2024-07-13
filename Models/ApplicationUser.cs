using Microsoft.AspNetCore.Identity;
using Task_4_Web_App_.Enums;

namespace Task_4_Web_App_.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime LastLoginTime { get; set; }
        public DateTime RegistrationTime { get; set; }
        public UserStatus Status { get; set; }

    }
}

