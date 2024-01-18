using BerryessaUnion.Domains.UserSetup;
using BerryessaUnion.IServices.RepositoryConfig;

namespace BerryessaUnion.IServices.UserSetup
{
    public interface IRoleRepoServices : IRepositoryBaseService<Role>
    {
        Role GetRoleByUserId(long UserId);
        Role GetRoleByRoleId(long RoleId);
        List<Role> GetAllRoles();
    }
}
