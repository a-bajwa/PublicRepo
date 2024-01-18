using BerryessaUnion.Dto.EmployeeSetup.EmployeeContractSetup;
using BerryessaUnion.Dto.EmployeeSetup.EmployeeJobDetailDetup;
using BerryessaUnion.Dto.EmployeeSetup.EmployeePaymentSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Dto.EmployeeSetup
{
    public class EmployeePutDto: EmployeePostDto
    {
        public int EmployeeID { get; set; }
        public EmployeeJobDetailDto EmployeeJobDetail { get; set; }
        public EmployeeContactDto EmployeeContract { get; set; }
        public EmployeePaymentDto EmployeePayment { get; set; }
    }
}
