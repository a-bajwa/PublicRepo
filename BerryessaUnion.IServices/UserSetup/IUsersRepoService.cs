using BerryessaUnion.Domains.UserSetup;
using BerryessaUnion.IServices.RepositoryConfig;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.IServices.UserSetup
{
    public interface IUsersRepoService : IRepositoryBaseService<User>
    {
        PagedList<User> GetPaginationList(PaginationModel paginationModel);
        PagedList<Role> GetPaginationRoleList(PaginationModel paginationModel);
        PagedList<User> GetPaginationListByCompany(PaginationModel paginationModel);
        User GetDetailsByEmail(string Email);
        NewSerialNumber NewSerialNumber();
        User GetDetailsById(long id);
        User GetDetails(string Email, string pasword);
        User GetById_Query(long id, long CompanyId);
        List<User> GetListByCompanyId(long id);
        void ChangePassword(User model);
        bool IsEmail(string email);
        void SoftDelete(User model);
        int TotalActiveUsers();
        int TotalUsers();
        void SaveUserRole(UserRole model);
        void UpdateUserRole(UserRole model);
        bool UsersVerification(string code, long Id);
        UserRole GetUserRoleById(long id);
        Role GetUserRoleByUserId(long UserId);
        List<Role> GetRoleList();


    }
}
