using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Dto.EmployeeSetup.EmployeeJobDetailDetup
{
    public class EmployeeJobDetailPostDto
    {
        public string? JobGroup { get; set; }
        public string? Type { get; set; }
        public string? JobType { get; set; }
        public string? Status { get; set; }
        public string? Department { get; set; }
        public string? PrimaryDepartment { get; set; }
        public string? BargainingGroup { get; set; }
        public string? Class { get; set; }
        public string? JobClassification { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
