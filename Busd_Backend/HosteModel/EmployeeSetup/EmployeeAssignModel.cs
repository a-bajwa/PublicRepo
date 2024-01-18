using BerryessaUnion.Domains.EmployeeSetup;
using BerryessaUnion.Dto.EmployeeSetup;
using SharedLibrary.CommonFunctions;
using SharedLibrary.Models;


namespace Busd_Backend.HosteModel.EmployeeSetup
{
    public static class EmployeeAssignModel
    {
        public static PaginationModel PaginationAssign(DataTablePostModel model)
        {
            PaginationModel paginationModel = new PaginationModel();
            paginationModel.PageSize = model.length;
            if (model.start != 0)
            {
                model.start = model.start / model.length;
                paginationModel.PageNumber = model.start + 1;
            }
            else
            {
                paginationModel.PageNumber = 1;
            }
           
            if (!String.IsNullOrEmpty(model.StartDate) && !String.IsNullOrEmpty(model.EndDate))
            {
                paginationModel.StartDate = CommonFunction.ConvertDateTimeUItoAPI(model.StartDate);
                paginationModel.EndDate = CommonFunction.ConvertDateTimeUItoAPI(model.EndDate);
            }
            paginationModel.SearchTerm = (model.search != null) ? model.search.value : null;
            paginationModel.OrderBy = model.order.columnstr;
            
            if (model.order.dirbool)
            {
                paginationModel.OrderBy += " ASC";
            }
            else
            {
                paginationModel.OrderBy += " DESC";
            }
            return paginationModel;

        }
        public static Employee UpdatedAssignModel(Employee model, EmployeePutDto putObj, long loginId)
        {
            model.FirstName = putObj.FirstName;
            model.LastName = putObj.LastName;
            model.Email = putObj.Email;
            model.FullName = putObj.LastName + ", " + putObj.FirstName;
            model.Gender = putObj.Gender;
            model.HireDate = putObj.HireDate;
            model.WorkEmail = putObj.WorkEmail;
            model.OriginalHireDate = putObj.OriginalHireDate;
            model.TerminationStatus = putObj.TerminationStatus;
            model.TerminationDate = putObj.TerminationDate;
            model.RetirementDate = putObj.RetirementDate;
            model.SSN = putObj.SSN;
            model.SSNLast4 = putObj.SSNLast4;
            model.PortalEmail = putObj.PortalEmail;
            model.Birthdate = putObj.Birthdate;
            model.MiddleInitial = putObj.MiddleInitial;

           
            model = UpdatedTracking(model, loginId);
            return model;
        }
        public static Employee CreatedTracking(Employee model, EmployeePostDto registerUser, long loginId)
        {
            System.Random random = new System.Random();
            model.FullName = model.FirstName + " " + model.LastName;
            //model.CreatedBy = loginId;
            //model.IsActive = model.IsActive;
            //model.CreatedAt = DateTime.Now;

            //model.CreatedAt = DateTime.Now;

            
            return model;
        }
        public static Employee UpdatedTracking(Employee model, long loginId)
        {
            //model.UpdatedBy = loginId;
            //model.UpdatedAt = DateTime.Now;
            return model;
        }
        public static Employee DeletedTracking(Employee model, long loginId)
        {
            //model.IsDeleted = true;
            //model.Username = model.Username + "_deleted";
            //model.Email = model.Email + "_deleted";
            //model.DeletedBy = loginId;
            //model.DeletedAt = DateTime.Now;
            return model;
        }
    }
}
