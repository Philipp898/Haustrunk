namespace Haustrunk.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<(IAsyncResult Result, string UserId)> CreateUserAsync(string userName, string password);
        Task<IAsyncResult> DeleteUserAsync(string userId);

    }
}
