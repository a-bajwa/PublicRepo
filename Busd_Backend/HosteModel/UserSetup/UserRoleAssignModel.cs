using BerryessaUnion.Domains.UserSetup;
using BerryessaUnion.Dto.UserRoles;
using SharedLibrary.CommonFunctions;
using SharedLibrary.Models;

namespace Busd_Backend.HosteModel.UserSetup
{
    public static class UserRoleAssignModel
    {
        public static UserRole UpdatedAssignModel(UserRole model, UserRolePutDto putObj)
        {
            model.UserId = putObj.UserId;
            model.RoleId = putObj.RoleId;
            return model;
        }
        public static UserRole CreateAssignModel(UserRole model, UserRolePutDto putObj)
        {
            model.UserId = putObj.UserId;
            model.RoleId = putObj.RoleId;
            return model;
        }
        //public static PaginationModel PaginationAssign(DataTableAjaxPostModel model)
        //{
        //    PaginationModel paginationModel = new PaginationModel();
        //    paginationModel.PageSize = model.length;
        //    if (model.start != 0)
        //    {
        //        model.start = model.start / model.length;
        //        paginationModel.PageNumber = model.start + 1;
        //    }
        //    else
        //    {
        //        paginationModel.PageNumber = 1;
        //    }

        //    if (!String.IsNullOrEmpty(model.StartDate) && !String.IsNullOrEmpty(model.EndDate))
        //    {
        //        paginationModel.StartDate = CommonFunction.ConvertDateTimeUItoAPI(model.StartDate);
        //        paginationModel.EndDate = CommonFunction.ConvertDateTimeUItoAPI(model.EndDate);
        //    }
        //    paginationModel.SearchTerm = (model.search != null) ? model.search.value : null;
        //    paginationModel.OrderBy = model.order.columnstr;
        //    if (model.order.dirbool)
        //    {
        //        paginationModel.OrderBy += " ASC";
        //    }
        //    else
        //    {
        //        paginationModel.OrderBy += " DESC";
        //    }

        //    return paginationModel;

        //}


    }
}
