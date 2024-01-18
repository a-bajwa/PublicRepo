using BerryessaUnion.Domains.EmployeeSetup;
using BerryessaUnion.Dto.EmployeeSetup;
using BerryessaUnion.Dto.EmployeeSetup.EmployeeContractSetup;
using BerryessaUnion.Dto.EmployeeSetup.EmployeePaymentSetup;

namespace Busd_Backend.HosteModel.EmployeeSetup
{
    public static class EmployeePaymentAssignModel
    {
        public static EmployeePayment UpdatedAssignModel(EmployeePayment model, EmployeePaymentDto putObj)
        {

            model.JobNumber = putObj.JobNumber;
            model.TotalSalary = putObj.TotalSalary;
            model.BaseSalary = putObj.BaseSalary;
            model.ExtraPay = putObj.ExtraPay;
            model.TotalSalary1 = putObj.TotalSalary1;
            model.FTE = putObj.FTE;
            model.HoursDay = putObj.HoursDay;
            
            return model;
        }
    }
}
