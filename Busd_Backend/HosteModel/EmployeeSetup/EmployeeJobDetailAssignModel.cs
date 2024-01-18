using BerryessaUnion.Domains.EmployeeSetup;
using BerryessaUnion.Dto.EmployeeSetup;
using BerryessaUnion.Dto.EmployeeSetup.EmployeeContractSetup;
using BerryessaUnion.Dto.EmployeeSetup.EmployeeJobDetailDetup;

namespace Busd_Backend.HosteModel.EmployeeSetup
{
    public static class EmployeeJobDetailAssignModel
    {
        public static EmployeeJobDetail UpdatedAssignModel(EmployeeJobDetail model, EmployeeJobDetailDto putObj)
        {

            model.JobGroup = putObj.JobGroup;
            model.BargainingGroup = putObj.BargainingGroup;
            model.Type = putObj.Type;
            model.JobType = putObj.JobType;
            model.Status = putObj.Status;
            model.Class = putObj.Class;
            model.Department = putObj.Department;
            model.PrimaryDepartment = putObj.PrimaryDepartment;
            model.BargainingGroup1 = putObj.BargainingGroup1;
            model.BargainingGroup2 = putObj.BargainingGroup2;
            model.JobClassification = putObj.JobClassification;
            model.StartDate = putObj.StartDate;
            model.EndDate = putObj.EndDate;
            model.Description = putObj.Description;
            return model;
        }
    }
}
