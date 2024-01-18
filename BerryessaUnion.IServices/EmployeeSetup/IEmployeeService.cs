using BerryessaUnion.Domains.EmployeeSetup;
using BerryessaUnion.IServices.RepositoryConfig;
using SharedLibrary.Models;
namespace BerryessaUnion.IServices.EmployeeSetup
{
    public interface IEmployeeService : IRepositoryBaseService<Employee>
    {
        PagedList<Employee> GetPaginationList(PaginationModel paginationModel);
        Employee GetDetailsByEmail(string Email);
        List<EmployeePayment> GetPaymentDetailsByEmployeeId(int EmployeeId);
        int NewSerialNumber();
        Employee GetDetailsById(int id);
        Employee GetDetails(string Email, string pasword);
        void ChangePassword(Employee model);
        bool IsEmail(string email);
        void SoftDelete(Employee model);
        int TotalActiveEmployee();
        int TotalEmployee();
    }
}
