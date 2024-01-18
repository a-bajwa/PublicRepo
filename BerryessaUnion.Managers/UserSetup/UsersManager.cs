using BerryessaUnion.Domains.UserSetup;
using BerryessaUnion.IServices.JwtServices;
using BerryessaUnion.IServices.UserSetup;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BerryessaUnion.Managers.UserSetup
{
    public class UsersManager : IUsersService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<User> _users;
        private readonly ISecurityService _securityService;
        private readonly IHttpContextAccessor _contextAccessor;

        public UsersManager(
            IUnitOfWork uow,
            ISecurityService securityService,
            IHttpContextAccessor contextAccessor)
        {
            _uow = uow;

            _users = _uow.Set<User>();

            _securityService = securityService;

            _contextAccessor = contextAccessor;
        }

        public ValueTask<User> FindUserAsync(long userId)
        {
            return _users.FindAsync(userId);
        }

        public async Task<User> FindUserAsync(string username, string password)
        {
            var passwordHash = _securityService.GetSha256Hash(password);
             var usr=await _users.Where(x => x.Email == username && x.PasswordHash == passwordHash && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();
            return usr;
        }

        public async Task<string> GetSerialNumberAsync(long userId)
        {
            var user = await FindUserAsync(userId);
            return user.SerialNumber;
        }

        public async Task UpdateUserLastActivityDateAsync(long userId)
        {
            var user = await FindUserAsync(userId);
            if (user.LastLoggedIn != null)
            {
                var updateLastActivityDate = TimeSpan.FromMinutes(2);
                var currentUtc = DateTime.UtcNow;
                var timeElapsed = currentUtc.Subtract(user.LastLoggedIn.Value);
                if (timeElapsed < updateLastActivityDate)
                {
                    return;
                }
            }
            user.LastLoggedIn = DateTime.UtcNow;
            await _uow.SaveChangesAsync();
        }

        public long GetCurrentUserId()
        {
            var claimsIdentity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userDataClaim = claimsIdentity?.FindFirst(ClaimTypes.UserData);
            var userId = userDataClaim?.Value;
            return string.IsNullOrWhiteSpace(userId) ? 0 : long.Parse(userId);
        }

        public ValueTask<User> GetCurrentUserAsync()
        {
            var userId = GetCurrentUserId();
            return FindUserAsync(userId);
        }

        public async Task<(bool Succeeded, string Error)> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            var currentPasswordHash = _securityService.GetSha256Hash(currentPassword);
            if (user.PasswordHash != currentPasswordHash)
            {
                return (false, "Current password is wrong.");
            }

            user.PasswordHash = _securityService.GetSha256Hash(newPassword);
            // user.SerialNumber = Guid.NewGuid().ToString("N"); // To force other logins to expire.
            await _uow.SaveChangesAsync();
            return (true, string.Empty);
        }
    }
}