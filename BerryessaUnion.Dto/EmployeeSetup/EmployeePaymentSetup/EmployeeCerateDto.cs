using BerryessaUnion.Dto.EmployeeSetup.EmployeeContractSetup;
using BerryessaUnion.Dto.EmployeeSetup.EmployeeJobDetailDetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Dto.EmployeeSetup.EmployeePaymentSetup
{
    public class EmployeeCerateDto
    {
        public EmployeePostDto EmployeeData { get; set; }
        public EmployeePaymentPostDto EmployeePaymentData { get; set; }
        public EmployeeJobDetailPostDto EmployeeJobDetailData { get; set; }
        public EmployeeContactPostDto EmployeeContractData { get; set; }
    }
}
