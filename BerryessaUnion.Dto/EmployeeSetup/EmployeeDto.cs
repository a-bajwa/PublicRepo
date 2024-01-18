using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerryessaUnion.Dto.EmployeeSetup.EmployeeContractSetup;
using BerryessaUnion.Dto.EmployeeSetup.EmployeeJobDetailDetup;
using BerryessaUnion.Dto.EmployeeSetup.EmployeePaymentSetup;

namespace BerryessaUnion.Dto.EmployeeSetup
{
    public class EmployeeDto
    {
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
         public EmployeeContactDto EmployeeContact { get; set; }
         public List<EmployeeJobDetailDto> EmployeeJobDetails { get; set; }
         public List<EmployeePaymentDto> EmployeePayments { get; set; }


    }
}
