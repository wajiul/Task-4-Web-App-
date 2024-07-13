using Task_4_Web_App_.DTOs;

namespace Task_4_Web_App_.Repositories
{
    public interface IUserRepository
    {
        Task BlockUsersAsync(IEnumerable<string> userIds);
        Task DeleteUsersAsync(IEnumerable<string> userIds);
        IEnumerable<UserInfoDto> GetAllUsers();
        Task SaveAsync();
        Task UnblockUsersAsync(IEnumerable<string> userIds);
    }
}