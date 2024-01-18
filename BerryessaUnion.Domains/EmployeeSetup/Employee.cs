using System.ComponentModel.DataAnnotations;

namespace BerryessaUnion.Domains.EmployeeSetup
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleInitial { get; set; }
        public string? FullName { get; set; }
        public DateTime? OriginalHireDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string? TerminationStatus { get; set; }
        public DateTime? TerminationDate { get; set; }
        public DateTime? RetirementDate { get; set; }
        public string? SSN { get; set; }
        public string? SSNLast4 { get; set; }
        public string? Email { get; set; }
        public string? WorkEmail { get; set; }
        public string? PortalEmail { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Gender { get; set; }
        public virtual EmployeeContact EmployeeContact { get; set; }        
        public virtual List<EmployeeJobDetail> EmployeeJobDetails { get; set; }        
        public virtual List<EmployeePayment> EmployeePayments { get; set; }        
    }
}
