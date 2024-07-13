using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Task_4_Web_App_.DbContext;
using Task_4_Web_App_.Models;
using Task_4_Web_App_.Repositories;

namespace Task_4_Web_App_.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        public readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        public readonly UserManager<ApplicationUser> _userManager;
        public UsersController(IUserRepository userRepository, UserManager<ApplicationUser> userManager, ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _accountRepository = accountRepository;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("users/getall")]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("users/block")]
        public async Task<IActionResult> BlockUsers([FromBody] List<string> userIds)
        {
            string currentUserId = _userManager.GetUserId(User);

            await _userRepository.BlockUsersAsync(userIds);
            await _userRepository.SaveAsync();

            if(userIds.Contains(currentUserId))
            {
                await _accountRepository.SignOutAsync();
                return Unauthorized();
            }

            return Ok();
        }

        [HttpPost("users/unblock")]
        public async Task<IActionResult> UnblockUsers([FromBody] List<string> userIds)
        {
            await _userRepository.UnblockUsersAsync(userIds);
            await _userRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("users/delete")]
        public async Task<IActionResult> DeleteUsers([FromBody] List<string> userIds)
        {
            string currentUserId = _userManager.GetUserId(User);

            await _userRepository.DeleteUsersAsync(userIds);
            await _userRepository.SaveAsync();

            if (userIds.Contains(currentUserId))
            {
                await _accountRepository.SignOutAsync();
                return Unauthorized();
            }

            return Ok();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
