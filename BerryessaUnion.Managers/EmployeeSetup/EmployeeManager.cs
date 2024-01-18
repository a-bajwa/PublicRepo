using Entity;
using SharedLibrary.Models;
using BerryessaUnion.Domains.EmployeeSetup;
using BerryessaUnion.IServices.EmployeeSetup;
using BerryessaUnion.Managers.ExtensionSetup;
using BerryessaUnion.Managers.RepositoryConfig;
using Microsoft.EntityFrameworkCore;


namespace BerryessaUnion.Managers.EmployeeSetup
{
    public class EmployeeManager : RepositoryBaseManager<Employee>, IEmployeeService
    {
        private ApplicationDbContext _apiDbContext;

        public EmployeeManager(ApplicationDbContext apiDbContext) : base(apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public PagedList<Employee> GetPaginationList(PaginationModel paginationModel)
        {
            try
            {
                IQueryable<Employee> obj;
                Employee employee = new Employee();
                if (String.IsNullOrEmpty(paginationModel.OrderBy) || paginationModel.OrderBy.Contains("string"))
                {
                    paginationModel.OrderBy = "Id";
                }
                if (paginationModel.StartDate != null && paginationModel.EndDate != null)
                {
                    obj = _apiDbContext.tblEmployee.Where(a => a.HireDate.Value.Date >= paginationModel.StartDate.Value.Date && a.HireDate.Value.Date <= paginationModel.EndDate.Value.Date)
                        .Search_Employees(paginationModel.SearchTerm)
                         .OrderByDescending(a => a.EmployeeID)
                        .OrderBy_Employees(paginationModel.OrderBy, paginationModel.OrderDir);
                    //.Include(a => a.EmployeeContact)
                    //.Include(x => x.EmployeeJobDetails);
                }
                else
                {
                    obj = _apiDbContext.tblEmployee.Where(a => a.EmployeeID > 0)
                        .Search_Employees(paginationModel.SearchTerm)
                        .OrderByDescending(a=> a.EmployeeID)
                        .OrderBy_Employees(paginationModel.OrderBy, paginationModel.OrderDir);
                    //.Include(a => a.EmployeeContact)
                    //.Include(x => x.EmployeeJobDetails);
                }
                return PagedList<Employee>.ToPagedList(obj, paginationModel.PageNumber, paginationModel.PageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EmployeePayment> GetPaymentDetailsByEmployeeId(int EmployeeId)
        {
            try
            {
                var result = _apiDbContext.tblEmployeePayment.Where(x => x.EmployeeID == EmployeeId).ToList();
                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            };


        }
        public void SoftDelete(Employee model)
        {
            try
            {
                _apiDbContext.tblEmployee.Update(model);
                _apiDbContext.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            };
        }
        public int TotalActiveEmployee()
        {
            return _apiDbContext.tblEmployee.Count();
        }
        public int TotalEmployee()
        {
            return _apiDbContext.tblEmployee.Count();
        }
        public bool UsersVerification(string code, long Id)
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        }
        public void ChangePassword(Employee model)
        {
            try
            {
                var findObj = _apiDbContext.tblEmployee.Where(a => a.EmployeeID == model.EmployeeID).FirstOrDefault();
                //if (findObj != null)
                //{
                //    findObj.PasswordHash = model.PasswordHash;
                //    findObj.Password = model.Password;
                //    _apiDbContext.Entry(findObj).State = EntityState.Modified;
                //    _apiDbContext.SaveChanges();
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Employee GetDetailsById(int id)
        {
            try
            {
                var result = _apiDbContext.tblEmployee.Where(a => a.EmployeeID == id)
                         .Include(a => a.EmployeeContact)
                         .Include(a => a.EmployeeJobDetails)
                         //.Include(a=> a.EmployeePayments)
                         .FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public Employee GetDetailsByEmail(string Email)
        {
            try
            {
                var result = _apiDbContext.tblEmployee.Where(a => a.Email == Email).Include(a => a.EmployeeContact).FirstOrDefault();
                return result;
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
                var result = _apiDbContext.tblEmployee.Where(a => a.Email == Email).FirstOrDefault();
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
        public int NewSerialNumber()
        {
            try
            {
                var result = _apiDbContext.tblEmployee.OrderByDescending(x => x.EmployeeID).FirstOrDefault();
                if (result != null)
                    return result.EmployeeID;
                else return 0;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Employee GetDetails(string Email, string pasword)
        {
            try
            {
                var result = _apiDbContext.tblEmployee.Where(a => a.Email == Email).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
