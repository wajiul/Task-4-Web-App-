using Microsoft.AspNetCore.Identity;
using Task_4_Web_App_.DbContext;
using Task_4_Web_App_.Models;

namespace Task_4_Web_App_.Repositories
{
    public class AccountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(UserRegistrationModel userData)
        {
            var emailExist = await _userManager.FindByEmailAsync(userData.Email); 
            if(emailExist != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email already exist" });
            }

            var newUser = new ApplicationUser()
            {
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Email = userData.Email,
                UserName = userData.Email,
                RegistrationTime = DateTime.Now,
                Status = "Active"
            };

            var result = await _userManager.CreateAsync(newUser, userData.Password);
            return result;
        }

        public async Task<SignInResult> SignInUserAsync(UserLoginModel userCredential)
        {
            var result = await _signInManager.PasswordSignInAsync(userCredential.Email, userCredential.Password, false, false);
            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
