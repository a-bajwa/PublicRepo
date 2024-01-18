using BerryessaUnion.Domains.UserSetup;
using BerryessaUnion.IServices.UserSetup;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharedLibrary.CommonFunctions.CommonEnums;

namespace BerryessaUnion.Managers.UserSetup
{
    public class RoleManager : IRoleService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<Role> _roles;
        private readonly DbSet<User> _users;
        //private ApplicationDbContext _apiDbContext;

        public RoleManager(IUnitOfWork uow)
        {
            _uow = uow;
            _roles = _uow.Set<Role>();
            _users = _uow.Set<User>();
        }

        //public Task<List<Role>> FindUserRolesAsync(long userId)
        //{
        //    var userRolesQuery = from role in _roles
        //                         from userRoles in role.UserRoles
        //                         where userRoles.UserId == userId
        //                         select role;
        //    return userRolesQuery.OrderBy(x => x.Name).ToListAsync();
        //}

        //public async Task<bool> IsUserInRoleAsync(long userId, string roleName)
        //{
        //    var userRolesQuery = from role in _roles
        //                         where role.Name == roleName
        //                         from user in role.UserRoles
        //                         where user.UserId == userId
        //                         select role;
        //    var userRole = await userRolesQuery.FirstOrDefaultAsync();
        //    return userRole != null;
        //}

        //public Task<List<User>> FindUsersInRoleAsync(string roleName)
        //{
        //    var roleUserIdsQuery = from role in _roles
        //                           where role.Name == roleName
        //                           from user in role.UserRoles
        //                           select user.UserId;
        //    return _users.Where(user => roleUserIdsQuery.Contains(user.Id))
        //                 .ToListAsync();
        //}

        public Role FindRolesAsync(RoleType RoleType)
        {


            var roleUserIdsQuery = from role in _roles
                                   where role.RoleType == RoleType
                                   select role;

            return roleUserIdsQuery.FirstOrDefault();

        }
    }
}
