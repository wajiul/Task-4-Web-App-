using System.ComponentModel.DataAnnotations;

namespace Task_4_Web_App_.Models
{
    public class UserLoginModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
