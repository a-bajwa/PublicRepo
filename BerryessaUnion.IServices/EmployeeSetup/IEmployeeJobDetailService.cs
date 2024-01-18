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
    public interface IEmployeeJobDetailService : IRepositoryBaseService<EmployeeJobDetail>
    {
        //PagedList<Employee> GetPaginationList(PaginationModel paginationModel);
        EmployeeJobDetail GetDetailsByEmployeeId(int Id);
        EmployeeJobDetail GetLastJobEmployeeId(int Id);
        NewSerialNumber NewSerialNumber();
        EmployeeJobDetail GetDetailsById(int Id);
       
    }
}
