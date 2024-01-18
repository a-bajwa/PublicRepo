using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Dto.UserRoles
{
    public class UserRolePostDto
    {
        [Required(ErrorMessage = "Please define user first")]
        public long UserId { get; set; }
        [Required(ErrorMessage = "Role is Required")]
        public long RoleId { get; set; }

    }
}
