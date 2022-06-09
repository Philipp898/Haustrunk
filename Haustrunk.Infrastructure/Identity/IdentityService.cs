using Haustrunk.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Haustrunk.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(bool, string UserId)> CreateUserAsync(string userName, string password, string email)
        {
            var user = new ApplicationUser()
            {
                UserName = userName,
                Email = email
            };
            var result = await _userManager.CreateAsync(user,password);

            return (result.Succeeded ,user.Id);
        }


        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null) throw new KeyNotFoundException();

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var test =  _userManager.Users.ToList();
            var user = await _userManager.Users.FirstAsync(user => user.Id == userId);
            return user.UserName;
        }
    }
}
