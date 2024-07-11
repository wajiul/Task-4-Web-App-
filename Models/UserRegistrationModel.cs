using System.ComponentModel.DataAnnotations;

namespace Task_4_Web_App_.Models
{
    public class UserRegistrationModel
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }  = string.Empty;
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password not matched")]
        public string ConfirmPassword { get; set; } = string.Empty; 

    }
}
