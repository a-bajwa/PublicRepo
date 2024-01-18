using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Domains.EmployeeSetup
{
    public class EmployeePayment
    {
        public int ID { get; set; }
        public int? EmployeeID { get; set; }
        public string? JobNumber { get; set; }
        public decimal? TotalSalary { get; set; }
        public decimal? FTE { get; set; }
        public float? TotalUnits { get; set; }
        public float? EarnedUnits { get; set; }
        public float? ApprovedUnits { get; set; }
        public string? Qualtrics { get; set; }
        public float? JobFTE { get; set; }
        public decimal? FTE1 { get; set; }
        public decimal? HoursDay { get; set; }
        public decimal? AllocationUnits { get; set; }
        public decimal? AllocationFTE { get; set; }
        public decimal? AllocationHoursDay { get; set; }
        public decimal? BaseSalary { get; set; }
        public decimal? ExtraPay { get; set; }
        public decimal? TotalSalary1 { get; set; }
        public decimal? Retirement { get; set; }
        public decimal? Statutory { get; set; }
        public decimal? HealthandWelfare { get; set; }
        public decimal? TotalBenefits { get; set; }
        public decimal? TotalCost { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employees { get; set; }
    }
}
