using BerryessaUnion.Domains.UserSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.IServices.UserSetup
{
    public interface IUsersService
    {
        Task<string> GetSerialNumberAsync(long userId);
        Task<User> FindUserAsync(string username, string password);
        ValueTask<User> FindUserAsync(long userId);
        Task UpdateUserLastActivityDateAsync(long userId);
        ValueTask<User> GetCurrentUserAsync();
        long GetCurrentUserId();
        Task<(bool Succeeded, string Error)> ChangePasswordAsync(User user, string currentPassword, string newPassword);
    }
}
