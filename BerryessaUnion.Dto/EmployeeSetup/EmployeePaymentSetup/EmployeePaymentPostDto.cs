using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Dto.EmployeeSetup.EmployeePaymentSetup
{
    public class EmployeePaymentPostDto
    {
        public string? JobNumber { get; set; }
        public decimal? TotalSalary { get; set; }
        public decimal? BaseSalary { get; set; }
        public decimal? ExtraPay { get; set; }
        public decimal? TotalSalary1 { get; set; }
        public decimal? FTE { get; set; }
        public decimal? HoursDay { get; set; }
    }
}
