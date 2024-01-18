using BerryessaUnion.Domains.EmployeeSetup;
using BerryessaUnion.IServices.EmployeeSetup;
using BerryessaUnion.IServices.UserSetup;
using BerryessaUnion.Managers.ExtensionSetup;
using BerryessaUnion.Managers.RepositoryConfig;
using Entity;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;

namespace BerryessaUnion.Managers.EmployeeSetup
{
    public class EmployeePaymentManager : RepositoryBaseManager<EmployeePayment>, IEmployeePaymentService
    {
        private ApplicationDbContext _apiDbContext;

        public EmployeePaymentManager(ApplicationDbContext apiDbContext) : base(apiDbContext)
        {
            _apiDbContext = apiDbContext;

        }

        //public PagedList<Employee> GetPaginationList(PaginationModel paginationModel)
        //{
        //    try
        //    {
        //        IQueryable<Employee> obj;
        //        if (String.IsNullOrEmpty(paginationModel.OrderBy) || paginationModel.OrderBy.Contains("name"))
        //        {
        //            paginationModel.OrderBy = "Id";
        //        }
        //        if (paginationModel.StartDate != null && paginationModel.EndDate != null)
        //        {
        //            obj = _apiDbContext.Employees.Where(a => a.IsDeleted == false && a.CreatedAt.Value.Date >= paginationModel.StartDate.Value.Date && a.CreatedAt.Value.Date <= paginationModel.EndDate.Value.Date).Search_Employees(paginationModel.SearchTerm);//.OrderBy(paginationModel.OrderBy);
        //        }
        //        else
        //        {
        //            obj = _apiDbContext.Employees.Where(a => a.IsDeleted == false).Search_Employees(paginationModel.SearchTerm).Search_Employee_Status(paginationModel.Status);//.OrderBy(paginationModel.OrderBy);
        //        }

        //        return PagedList<Employee>.ToPagedList(obj, paginationModel.PageNumber, paginationModel.PageSize);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


        
       
        public EmployeePayment GetDetailsById(int id)
        {
            return _apiDbContext.tblEmployeePayment.Where(a => a.ID == id).FirstOrDefault();
        }

        public EmployeePayment GetLastPaymentEmployeeById(int id)
        {
            return _apiDbContext.tblEmployeePayment.Where(a => a.EmployeeID == id).OrderBy(a => a.ID).LastOrDefault();
        }


        public List<EmployeePayment> GetDetailsByEmployeeId(int EmployeeId)
        {
            return _apiDbContext.tblEmployeePayment.Where(a => a.EmployeeID == EmployeeId).ToList();
        }


        
        public NewSerialNumber NewSerialNumber()
        {
            //int srNo = 0;
            //var list = (from max in _apiDbContext.EmployeesCompany
            //            where max.SrNo != 0
            //            select max.SrNo).ToList();
            //int maxvalue = 0;
            //if (list.Count() != 0)
            //    maxvalue = (int)list.Max();
            //if (maxvalue == 0)
            //{
            //    srNo = 1;
            //}
            //else
            //{
            //    srNo = maxvalue + 1;
            //}
            NewSerialNumber Model = new NewSerialNumber()
            {
                SrNo = 1,
                Code = 1.ToString()
            };
            return Model;
        }

        


    }
}
