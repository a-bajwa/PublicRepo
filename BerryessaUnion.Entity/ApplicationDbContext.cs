using BerryessaUnion.Domains.EmployeeSetup;
using BerryessaUnion.Domains.FileSetup;
using BerryessaUnion.Domains.MenuSetup;
using BerryessaUnion.Domains.UserSetup;
using Microsoft.EntityFrameworkCore;

namespace Entity
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }
        public virtual DbSet<Menu> Menu { set; get; }
        public virtual DbSet<User> WebUsers { set; get; }
        public virtual DbSet<Role> WebRoles { set; get; }
        public virtual DbSet<FileToUpload> Files { set; get; }
        public virtual DbSet<UserRole> UserRoles { set; get; }
        public virtual DbSet<UserToken> WebUserTokens { set; get; }
        public virtual DbSet<UserAddres> WebUserAddress { set; get; }
        public virtual DbSet<Employee> tblEmployee { set; get; }
        public virtual DbSet<EmployeeContact> tblEmployeeContact { set; get; }
        public virtual DbSet<EmployeeJobDetail> tblEmployeeJobDetails { set; get; }
        public virtual DbSet<EmployeePayment> tblEmployeePayment { set; get; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // it should be placed here, otherwise it will rewrite the following settings!
            base.OnModelCreating(builder);
        }
    }
}
