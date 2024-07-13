using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task_4_Web_App_.DbContext;
using Task_4_Web_App_.DTOs;
using Task_4_Web_App_.Enums;
using Task_4_Web_App_.Models;

namespace Task_4_Web_App_.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IEnumerable<UserInfoDto> GetAllUsers()
        {
            var appUsers = _userManager.Users;
            var usersData = new List<UserInfoDto>();
            foreach (var user in appUsers)
            {
                usersData.Add(new UserInfoDto
                {
                    Id = user.Id,
                    Name = string.Concat(user.FirstName, " ", user.LastName),
                    Email = user.Email,
                    RegistrationTime = user.RegistrationTime.ToString("d MMMM, yyyy  hh:mm tt"),
                    LastLoginTime = user.LastLoginTime.ToString("d MMMM, yyyy  hh:mm tt"),
                    Status = user.Status.ToString()
                });
            }
            return usersData;
        }

        public async Task BlockUsersAsync(IEnumerable<string> userIds)
        {
            foreach (var Id in userIds)
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user != null)
                {
                    user.Status = UserStatus.Blocked;
                }
            }
        }
        public async Task UnblockUsersAsync(IEnumerable<string> userIds)
        {
            foreach (var Id in userIds)
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user != null)
                {
                    user.Status = UserStatus.Active;
                }
            }
        }

        public async Task DeleteUsersAsync(IEnumerable<string> userIds)
        {
            foreach (var Id in userIds)
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
