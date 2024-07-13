using Microsoft.AspNetCore.Identity;
using Task_4_Web_App_.DbContext;
using Task_4_Web_App_.Enums;
using Task_4_Web_App_.Models;

namespace Task_4_Web_App_.Repositories
{
    public class AccountRepository : IAccountRepository
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
            if (emailExist != null)
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
                Status = UserStatus.Active
            };

            return await _userManager.CreateAsync(newUser, userData.Password);
        }

        public async Task<SignInResult> SignInUserAsync(UserLoginModel userCredential)
        {
            return await _signInManager.PasswordSignInAsync(userCredential.Email, userCredential.Password, false, false);
        }
        public async Task UpdateLoginTime(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                user.LastLoginTime = DateTime.Now;
            }
        }
        public async Task<bool> IsUserBlockedAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return (user != null && user.Status == UserStatus.Blocked);

        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
