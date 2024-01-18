using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Dto.UsersSetup
{
    public class UserPostDto
    {
        //public Nullable<int> SrNo { get; set; }
        //public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        //public Nullable<bool> EmailConfirmed { get; set; }
        //public Nullable<bool> IsPasswordReset { get; set; }
        public string Password { get; set; }
        //public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        //public string ImagePath { get; set; }
        //public string ThumbNailPath { get; set; }
        //public Nullable<DateTime> LastLoggedIn { get; set; }
        //public string SerialNumber { get; set; }
        //public string VerificationCode { get; set; }
        //[Required(ErrorMessage = "Role is required")]
        public long RoleId { get; set; }
    }
}
