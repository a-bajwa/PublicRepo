using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Dto.UserRoles
{
    public class UserRoleDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        // public UserDto user { get; set; }
        public long RoleId { get; set; }
       // public Nullable<Guid> DesignationId { get; set; }
        //public RoleDto Role { get; set; }
    }
}
