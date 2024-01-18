using BerryessaUnion.Domains.UserSetup;
using BerryessaUnion.IServices.UserSetup;
using BerryessaUnion.Managers.RepositoryConfig;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Managers.UserSetup
{
    public class RoleRepoManager : RepositoryBaseManager<Role>, IRoleRepoServices
    {
        private ApplicationDbContext _apiDbContext;

        public RoleRepoManager(ApplicationDbContext apiDbContext) : base(apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public Role GetRoleByUserId(long UserId)
        {
            try
            {
                var user = _apiDbContext.WebUsers.Where(x => x.Id == UserId).FirstOrDefault();
                if (user != null)
                {
                    if (user.RoleId != null && user.RoleId > 0)
                    { return _apiDbContext.WebRoles.Where(a => a.Id == user.RoleId).FirstOrDefault(); }
                    return new Role();
                }
                else
                    return new Role();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Role GetRoleByRoleId(long RoleId)
        {
            try
            {
                return _apiDbContext.WebRoles.Where(a => a.Id == RoleId).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public List<Role> GetAllRoles()
        {
            try
            {
                return _apiDbContext.WebRoles.Where(a => a.Id > 0).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
