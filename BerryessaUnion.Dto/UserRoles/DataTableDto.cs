using BerryessaUnion.Dto.EmployeeSetup;
using BerryessaUnion.Dto.UsersSetup;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Dto.UserRoles
{
    public class UserDataTableDto : BaseTableDto
    {
        public List<UserDto> Data { get; set; }
    }
}
