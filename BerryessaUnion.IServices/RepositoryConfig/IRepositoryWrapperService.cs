using BerryessaUnion.IServices.EmployeeSetup;
using BerryessaUnion.IServices.UserSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.IServices.RepositoryConfig
{
    public interface IRepositoryWrapperService
    {
        void Save();
        IUsersRepoService UsersRepoService { get; }
        IRoleRepoServices RoleRepoServices { get; }
        IEmployeeService EmployeeServices { get; }
        IEmployeeContactService EmployeeContactServices { get; }
        IEmployeeJobDetailService EmployeeJobDetailServices { get; }
        IEmployeePaymentService EmployeePaymentServices { get; }
    }
}
