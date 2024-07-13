using Microsoft.AspNetCore.Mvc;
using Task_4_Web_App_.Models;
using Task_4_Web_App_.Repositories;

namespace Task_4_Web_App_.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepository _accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel loginData)
        {
            if(!ModelState.IsValid)
                return View(loginData);

            if(await _accountRepository.IsUserBlockedAsync(loginData.Email))
            {
                ModelState.AddModelError("", "User is blocked");
                return View(loginData);
            }

            if(await TrySignInUser(loginData))
            {
                return RedirectToAction("Index", "Users");  
            }

            ModelState.AddModelError("", "wrong username / password");

            return View(loginData);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegistrationModel userData)
        {
            if(!ModelState.IsValid) 
                return View(userData);

            var result = await _accountRepository.CreateUserAsync(userData);

            if(result.Succeeded)
                return RedirectToAction(nameof(Login));

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(userData);
        }

        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        private async Task<bool> TrySignInUser(UserLoginModel loginData)
        {
            var result = await _accountRepository.SignInUserAsync(loginData);
            if (result.Succeeded)
            {
                await _accountRepository.UpdateLoginTime(loginData.Email);
                await _accountRepository.SaveAsync();
                return true;
            }
            return false;
        }
    }
}

