namespace Haustrunk.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<(bool, string UserId)> CreateUserAsync(string userName, string password, string email);
        Task<bool> DeleteUserAsync(string userId);

    }
}
    