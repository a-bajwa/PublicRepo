using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SharedLibrary.CommonFunctions
{
    public static class CommonEnums
    {
        public enum ResponseType
        {
            Success = 1,
            Failure = 2,
            SuccessId = 3,
        }
        public enum RoleType
        {
            SuperAdmin = 1,
            Admin = 2,
            Employee = 3,
            HRManager = 4,
        }

        public enum EmailTemplateName
        {
            Register,
            Forgot,
            Contactus,
            ReceiverEmail,
            NewUser
        }
      
    }
}
