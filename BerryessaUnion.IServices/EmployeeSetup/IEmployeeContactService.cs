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
    public interface IEmployeeContactService : IRepositoryBaseService<EmployeeContact>
    {
        //PagedList<Employee> GetPaginationList(PaginationModel paginationModel);
        EmployeeContact GetDetailsByEmployeeId(int Id);
         EmployeeContact GetLastContractByEmployeeId(int EmployeeId);
        NewSerialNumber NewSerialNumber();
        EmployeeContact GetDetailsById(int Id);
       
    }
}
