using SharedLibrary.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Domains.UserSetup
{
    public class User : BaseModel
    {

       
        public Nullable<int> SrNo { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Nullable<bool> EmailConfirmed { get; set; }
        public Nullable<bool> IsPasswordReset { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public string ThumbNailPath { get; set; }
        public Nullable<DateTime> LastLoggedIn { get; set; }
        public string SerialNumber { get; set; }
        public string VerificationCode { get; set; }
        public Nullable<long> RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        //public virtual ICollection<UserRole> UserRoles { get; set; }
        //public virtual ICollection<UserToken> UserTokens { get; set; }

    }
}
