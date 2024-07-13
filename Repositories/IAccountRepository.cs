using Microsoft.AspNetCore.Identity;
using Task_4_Web_App_.Models;

namespace Task_4_Web_App_.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(UserRegistrationModel userData);
        Task<bool> IsUserBlockedAsync(string email);
        Task SaveAsync();
        Task<SignInResult> SignInUserAsync(UserLoginModel userCredential);
        Task SignOutAsync();
        Task UpdateLoginTime(string email);
    }
}