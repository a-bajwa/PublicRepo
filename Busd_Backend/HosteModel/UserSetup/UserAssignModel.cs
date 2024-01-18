using BerryessaUnion.Domains.UserSetup;
using BerryessaUnion.Dto.UsersSetup;
using SharedLibrary.CommonFunctions;

namespace Busd_Backend.HosteModel.UserSetup
{
    public static class UserAssignModel
    {

        public static User UpdatedRoleAssignModel(User model, long RoleId, long loginId)
        {
            model.RoleId= RoleId;
            model = UpdatedTracking(model, loginId);
            return model;
        }
        public static User UpdatedAssignModel(User model, UserPutDto putObj, long loginId)
        {
            model.FirstName = putObj.FirstName;
            model.LastName = putObj.LastName;
            model.DisplayName = putObj.FirstName + " " + putObj.LastName;
            model.PhoneNumber = putObj.PhoneNumber;
            model.Address = putObj.Address;
            //model.CompanyId = putObj.CompanyId;
            //model.Email = putObj.Email;
            model.IsActive = putObj.IsActive;
            model = UpdatedTracking(model, loginId);
            return model;
        }
        public static User CreatedTracking(User model, UserPostDto registerUser, long loginId)
        {
            System.Random random = new System.Random();
            model.EmailConfirmed = true;
            model.SerialNumber = random.Next().ToString();
            model.DisplayName = model.FirstName + " " + model.LastName;
            //model.CreatedBy = loginId;
            model.IsActive = model.IsActive;
            model.CreatedAt = DateTime.Now;

            model.CreatedAt = DateTime.Now;

            model.VerificationCode = CommonFunction.GenerateRandomNo();
            return model;
        }
        public static User UpdatedTracking(User model, long loginId)
        {
            //model.UpdatedBy = loginId;
            model.UpdatedAt = DateTime.Now;
            return model;
        }
        public static User DeletedTracking(User model, long loginId)
        {
            model.IsDeleted = true;
            //model.Username = model.Username + "_deleted";
            //model.Email = model.Email + "_deleted";
            model.DeletedBy = loginId;
            model.DeletedAt = DateTime.Now;
            return model;
        }
    }
}
