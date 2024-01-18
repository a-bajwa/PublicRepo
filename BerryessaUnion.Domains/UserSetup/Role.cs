using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharedLibrary.CommonFunctions.CommonEnums;

namespace BerryessaUnion.Domains.UserSetup
{
    public class Role
    {
        
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Modules { get; set; }
        public Nullable<RoleType> RoleType { get; set; }
        public Nullable<bool> AdminAccess { get; set; }
        public Nullable<bool> OwnerAccess { get; set; }
        //public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
