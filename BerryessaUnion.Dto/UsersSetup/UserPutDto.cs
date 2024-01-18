using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Dto.UsersSetup
{
    public class UserPutDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
       // public string ImagePath { get; set; }
        //public string ThumbNailPath { get; set; }
        //public string SerialNumber { get; set; }
        public long RoleId { get; set; }
    }
}
