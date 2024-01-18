using BerryessaUnion.Domains.UserSetup;
using BerryessaUnion.IServices.RepositoryConfig;
using BerryessaUnion.IServices.UserSetup;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Managers.UserSetup
{
    public class TestRepoManager : IUsersRepoService
    {
        void IUsersRepoService.ChangePassword(User model)
        {
            throw new NotImplementedException();
        }

        void IRepositoryBaseService<User>.Create(User entity)
        {
            throw new NotImplementedException();
        }

        User IRepositoryBaseService<User>.CreateEntity(User entity)
        {
            throw new NotImplementedException();
        }

        void IRepositoryBaseService<User>.CreateRange(List<User> entity)
        {
            throw new NotImplementedException();
        }

        void IRepositoryBaseService<User>.Delete(User entity)
        {
            throw new NotImplementedException();
        }

        IQueryable<User> IRepositoryBaseService<User>.FindByCondition(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        IQueryable<User> IRepositoryBaseService<User>.GetAll(Expression<Func<User, object>> expression)
        {
            throw new NotImplementedException();
        }

        IQueryable<User> IRepositoryBaseService<User>.GetAll()
        {
            throw new NotImplementedException();
        }

        User IUsersRepoService.GetById_Query(long id, long CompanyId)
        {
            throw new NotImplementedException();
        }

        User IUsersRepoService.GetDetails(string Email, string pasword)
        {
            throw new NotImplementedException();
        }

        User IUsersRepoService.GetDetailsByEmail(string Email)
        {
            throw new NotImplementedException();
        }

        User IUsersRepoService.GetDetailsById(long id)
        {
            throw new NotImplementedException();
        }

        List<User> IUsersRepoService.GetListByCompanyId(long id)
        {
            throw new NotImplementedException();
        }

        PagedList<User> IUsersRepoService.GetPaginationList(PaginationModel paginationModel)
        {
            throw new NotImplementedException();
        }

        PagedList<User> IUsersRepoService.GetPaginationListByCompany(PaginationModel paginationModel)
        {
            throw new NotImplementedException();
        }

        PagedList<Role> IUsersRepoService.GetPaginationRoleList(PaginationModel paginationModel)
        {
            throw new NotImplementedException();
        }

        List<Role> IUsersRepoService.GetRoleList()
        {
            throw new NotImplementedException();
        }

        UserRole IUsersRepoService.GetUserRoleById(long id)
        {
            throw new NotImplementedException();
        }

        Role IUsersRepoService.GetUserRoleByUserId(long UserId)
        {
            throw new NotImplementedException();
        }

        bool IUsersRepoService.IsEmail(string email)
        {
            throw new NotImplementedException();
        }

        NewSerialNumber IUsersRepoService.NewSerialNumber()
        {
            throw new NotImplementedException();
        }

        void IUsersRepoService.SaveUserRole(UserRole model)
        {
            throw new NotImplementedException();
        }

        void IUsersRepoService.SoftDelete(User model)
        {
            throw new NotImplementedException();
        }

        int IUsersRepoService.TotalActiveUsers()
        {
            throw new NotImplementedException();
        }

        int IUsersRepoService.TotalUsers()
        {
            throw new NotImplementedException();
        }

        void IRepositoryBaseService<User>.Update(User entity)
        {
            throw new NotImplementedException();
        }

        User IRepositoryBaseService<User>.UpdateEntity(User entity)
        {
            throw new NotImplementedException();
        }

        void IRepositoryBaseService<User>.UpdateRange(List<User> entity)
        {
            throw new NotImplementedException();
        }

        void IUsersRepoService.UpdateUserRole(UserRole model)
        {
            throw new NotImplementedException();
        }

        bool IUsersRepoService.UsersVerification(string code, long Id)
        {
            throw new NotImplementedException();
        }
    }
}
