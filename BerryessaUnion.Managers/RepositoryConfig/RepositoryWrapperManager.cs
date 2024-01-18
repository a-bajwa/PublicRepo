using BerryessaUnion.Domains.EmployeeSetup;
using BerryessaUnion.IServices.EmployeeSetup;
using BerryessaUnion.IServices.RepositoryConfig;
using BerryessaUnion.IServices.UserSetup;
using BerryessaUnion.Managers.EmployeeSetup;
using BerryessaUnion.Managers.UserSetup;
using Entity;

namespace BerryessaUnion.Managers.RepositoryConfig
{
    public class RepositoryWrapperManager : IRepositoryWrapperService
    {
        private readonly ApplicationDbContext _apiDbContext;
        private IUsersRepoService _iUsersRepoService;
        private IRoleRepoServices _iRoleRepoService;
        private IEmployeeService _iEmployeeService;
        private IEmployeeContactService _iEmployeeContactServices;
        private IEmployeePaymentService _iEmployeePaymentServices;
        private IEmployeeJobDetailService _iIEmployeeJobDetailServices;

        public RepositoryWrapperManager(ApplicationDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;

        }
        public void Save()
        {
            _apiDbContext.SaveChanges();
        }
        public IUsersRepoService UsersRepoService
        {
            get
            {
                if (_iUsersRepoService == null)
                {
                    _iUsersRepoService = new UsersRepoManager(_apiDbContext);
                }

                return _iUsersRepoService;
            }
        }
        public IEmployeeService EmployeeServices
        {
            get
            {
                if (_iEmployeeService == null)
                {
                    _iEmployeeService = new EmployeeManager(_apiDbContext);
                }

                return _iEmployeeService;
            }
        }
        public IEmployeeContactService EmployeeContactServices
        {
            get
            {
                if (_iEmployeeContactServices == null)
                {
                    _iEmployeeContactServices = new EmployeeContactManager(_apiDbContext);
                }

                return _iEmployeeContactServices;
            }
        }




        public IEmployeeJobDetailService EmployeeJobDetailServices
        {
            get
            {
                if (_iIEmployeeJobDetailServices == null)
                {
                    _iIEmployeeJobDetailServices = new EmployeeJobDetailManager(_apiDbContext);
                }

                return _iIEmployeeJobDetailServices;
            }
        }



        public IEmployeePaymentService EmployeePaymentServices
        {
            get
            {
                if (_iEmployeePaymentServices == null)
                {
                    _iEmployeePaymentServices = new EmployeePaymentManager(_apiDbContext);
                }

                return _iEmployeePaymentServices;
            }
        }

        public IRoleRepoServices RoleRepoServices
        {
            get
            {
                if (_iRoleRepoService == null)
                {
                    _iRoleRepoService = new RoleRepoManager(_apiDbContext);
                }

                return _iRoleRepoService;
            }
        }

    }
}