using Microsoft.AspNetCore.Mvc;
using Task_4_Web_App_.Models;

namespace Task_4_Web_App_.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(UserLoginModel loginData)
        {
            if(!ModelState.IsValid)
                return View(loginData);

            return View("login successfull");
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UserRegistrationModel userData)
        {
            if(!ModelState.IsValid) 
                return View(userData);

            return RedirectToAction(nameof(SignIn));
        }
    }
}

