using BerryessaUnion.Domains.EmployeeSetup;
using BerryessaUnion.Domains.UserSetup;
using BerryessaUnion.IServices.RepositoryConfig;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.IServices.EmployeeSetup
{
    public interface IEmployeePaymentService : IRepositoryBaseService<EmployeePayment>
    {
        //PagedList<Employee> GetPaginationList(PaginationModel paginationModel);
        List<EmployeePayment> GetDetailsByEmployeeId(int Id);
        NewSerialNumber NewSerialNumber();
        EmployeePayment GetDetailsById(int Id);
        EmployeePayment GetLastPaymentEmployeeById(int Id);
       
    }
}
