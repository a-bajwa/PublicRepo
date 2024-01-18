using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharedLibrary.CommonFunctions.CommonEnums;

namespace BerryessaUnion.Dto.UserRoles
{
    public class RolePostDto
    {
        public string Name { get; set; }
        public string Modules { get; set; }
        public Nullable<RoleType> RoleType { get; set; }
        public Nullable<bool> AdminAccess { get; set; }
        public Nullable<bool> OwnerAccess { get; set; }
    }
}
