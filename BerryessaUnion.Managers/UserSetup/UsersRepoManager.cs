using BerryessaUnion.Domains.UserSetup;
using BerryessaUnion.IServices.UserSetup;
using BerryessaUnion.Managers.ExtensionSetup;
using BerryessaUnion.Managers.RepositoryConfig;
using Entity;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;

namespace BerryessaUnion.Managers.UserSetup
{
    public class UsersRepoManager : RepositoryBaseManager<User>, IUsersRepoService
    {
        private ApplicationDbContext _apiDbContext;

        public UsersRepoManager(ApplicationDbContext apiDbContext) : base(apiDbContext)
        {
            _apiDbContext = apiDbContext;

        }
        public List<Role> GetRoleList()
        {
            List<Role> roles = new List<Role>();
            try
            {
                 roles = _apiDbContext.WebRoles.Where(x=> x. Id> 0).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return roles;
        }
        public PagedList<User> GetPaginationList(PaginationModel paginationModel)
        {
            IQueryable<User> obj;
            if (String.IsNullOrEmpty(paginationModel.OrderBy) || paginationModel.OrderBy.Contains("name"))
            {
                paginationModel.OrderBy = "Id";
            }
            if (paginationModel.StartDate != null && paginationModel.EndDate != null)
            {
                obj = _apiDbContext.WebUsers.Where(a => a.IsDeleted == false && a.CreatedAt.Value.Date >= paginationModel.StartDate.Value.Date && a.CreatedAt.Value.Date <= paginationModel.EndDate.Value.Date)
                    .Search_User(paginationModel.SearchTerm)
                    .Search_User_Status(paginationModel.Status)
                    .OrderBy_User(paginationModel.OrderBy, paginationModel.OrderDir);
            }
            else
            {
                obj = _apiDbContext.WebUsers.Where(a => a.IsDeleted == false)
                    .Search_User(paginationModel.SearchTerm)
                    .Search_User_Status(paginationModel.Status)
                    .OrderBy_User(paginationModel.OrderBy, paginationModel.OrderDir);
            }
            return PagedList<User>.ToPagedList(obj, paginationModel.PageNumber, paginationModel.PageSize);
        }
        public PagedList<Role> GetPaginationRoleList(PaginationModel paginationModel)
        {

            IQueryable<Role> obj;
            if (String.IsNullOrEmpty(paginationModel.OrderBy) || paginationModel.OrderBy.Contains("name") || paginationModel.OrderBy.Contains("srNo"))
            {
                paginationModel.OrderBy = "Id";
            }
            if (paginationModel.StartDate != null && paginationModel.EndDate != null)
            {
                obj = _apiDbContext.WebRoles.Search_Role(paginationModel.SearchTerm);//.OrderBy(paginationModel.OrderBy);
            }
            else
            {
                obj = _apiDbContext.WebRoles.Search_Role(paginationModel.SearchTerm);//.OrderBy(paginationModel.OrderBy);
            }
            return PagedList<Role>.ToPagedList(obj, paginationModel.PageNumber, paginationModel.PageSize);

        }
        public PagedList<User> GetPaginationListByCompany(PaginationModel paginationModel)
        {

            IQueryable<User> obj;
            if (String.IsNullOrEmpty(paginationModel.OrderBy) || paginationModel.OrderBy.Contains("name"))
            {
                paginationModel.OrderBy = "Id";
            }
            if (paginationModel.StartDate != null && paginationModel.EndDate != null)
            {
                obj = _apiDbContext.WebUsers.Where(a => a.IsDeleted == false).Where(a => a.CreatedAt.Value.Date >= paginationModel.StartDate.Value.Date && a.CreatedAt.Value.Date <= paginationModel.EndDate.Value.Date).Search_User(paginationModel.SearchTerm).OrderBy_User(paginationModel.OrderBy, paginationModel.OrderDir);//.OrderBy(paginationModel.OrderBy);
            }
            else
            {
                obj = _apiDbContext.WebUsers.Where(a => a.IsDeleted == false).Search_User(paginationModel.SearchTerm).Search_User_Status(paginationModel.Status).OrderBy_User(paginationModel.OrderBy, paginationModel.OrderDir);
            }
            return PagedList<User>.ToPagedList(obj, paginationModel.PageNumber, paginationModel.PageSize);

        }
        public void SoftDelete(User model)
        {
            _apiDbContext.WebUsers.Update(model);
            _apiDbContext.SaveChanges();
        }
        public int TotalActiveUsers()
        {
            return _apiDbContext.WebUsers.Where(a => a.IsDeleted == false && a.IsActive == true).Count();
        }
        public int TotalUsers()
        {
            return _apiDbContext.WebUsers.Where(a => a.IsDeleted == false).Count();
        }
        public bool UsersVerification(string code, long Id)
        {
            bool isCompleted = false;
            var findObj = _apiDbContext.WebUsers.Where(a => a.VerificationCode == code && a.Id == Id).FirstOrDefault();
            if (findObj != null)
            {
                findObj.EmailConfirmed = true;
                findObj.IsActive = true;
                findObj.VerificationCode = null;
                _apiDbContext.Update(findObj);
                _apiDbContext.SaveChanges();
                isCompleted = true;
            }
            return isCompleted;
        }
        public void ChangePassword(User model)
        {
            var findObj = _apiDbContext.WebUsers.Where(a => a.Id == model.Id).FirstOrDefault();
            findObj.PasswordHash = model.PasswordHash;
            findObj.Password = model.Password;
            _apiDbContext.Entry(findObj).State = EntityState.Modified;
            _apiDbContext.SaveChanges();
        }
        public User GetDetailsById(long id)
        {
            return _apiDbContext.WebUsers.Where(a => a.Id == id).FirstOrDefault();
        }
        public User GetById_Query(long id, long CompanyId)
        {
            return _apiDbContext.WebUsers.Where(a => a.Id == id).FirstOrDefault();
        }
        public List<User> GetListByCompanyId(long id)
        {
            return _apiDbContext.WebUsers.ToList();
        }
        public User GetDetailsByEmail(string Email)
        {
            return _apiDbContext.WebUsers.Where(a => a.Email == Email).FirstOrDefault();
        }
        public UserRole GetUserRoleById(long UserId)
        {
            return _apiDbContext.UserRoles.Where(a => a.UserId == UserId).FirstOrDefault();
        }
        public Role GetUserRoleByUserId(long UserId)
        {
            try
            {
                var user = _apiDbContext.WebUsers.Where(x => x.Id == UserId).FirstOrDefault();
                if (user != null)
                    return _apiDbContext.WebRoles.Where(a => a.Id == user.Id).FirstOrDefault();
                else
                    return new Role();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool IsEmail(string Email)
        {
            try
            {

                User result = _apiDbContext.WebUsers.Where(a => a.Email == Email).FirstOrDefault();
                if (result == null)
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {

                return true;
            }
        }
        public void SaveUserRole(UserRole model)
        {
            _apiDbContext.UserRoles.Add(model);
            _apiDbContext.SaveChanges();
        }
        public void UpdateUserRole(UserRole model)
        {
            _apiDbContext.UserRoles.Update(model);

        }
        public User GetDetails(string email, string password)
        {
            //var passwordHash = _securityService.GetSha256Hash(password);
            var usr = _apiDbContext.WebUsers.Where(x => x.Email == email && x.Password == password && x.IsDeleted == false && x.IsActive == true).FirstOrDefault();
            return usr;
        }
        public NewSerialNumber NewSerialNumber()
        {
            int srNo = 0;
            var list = (from max in _apiDbContext.WebUsers
                        where max.SrNo != 0
                        select max.SrNo).ToList();
            int maxvalue = 0;
            if (list.Count() != 0)
                maxvalue = (int)list.Max();
            if (maxvalue == 0)
            {
                srNo = 1;
            }
            else
            {
                srNo = maxvalue + 1;

            }
            NewSerialNumber Model = new NewSerialNumber()
            {
                SrNo = srNo,
                Code = srNo.ToString()

            };
            return Model;
        }
        //public List<Dropdown> GetRoleList()
        //{
        //    List<Dropdown> _objList = new List<Dropdown>();
        //    _objList = _apiDbContext.Roles.Where(a => a.RoleType != RoleType.SuperAdmin).Select(a => new Dropdown
        //    {
        //        label = a.Name,
        //        value = a.Id
        //    }).OrderBy(a => a.label).ToList();

        //    return _objList;
        //}
        //public List<Dropdown> LoadEmployeeRole()
        //{
        //    List<Dropdown> _objList = new List<Dropdown>();
        //    _objList = _apiDbContext.Roles.Where(a => a.RoleType == RoleType.Employee).Select(a => new Dropdown
        //    {
        //        label = a.Name,
        //        value = a.Id
        //    }).OrderBy(a => a.label).ToList();

        //    return _objList;
        //}

    }
}
