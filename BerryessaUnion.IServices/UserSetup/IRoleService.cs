using BerryessaUnion.Domains.UserSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharedLibrary.CommonFunctions.CommonEnums;

namespace BerryessaUnion.IServices.UserSetup
{
    public interface IRoleService
    {
        //Task<List<Role>> FindUserRolesAsync(long userId);
        Role FindRolesAsync(RoleType roletype);

        //Task<bool> IsUserInRoleAsync(long userId, string roleName);
        //Task<List<User>> FindUsersInRoleAsync(string roleName);
    }
}
