using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Domains.UserSetup
{
    public class UserRole
    {
        [Key]

        public long Id { get; set; }
        public Nullable<long> UserId { get; set; }
        public Nullable<long> RoleId { get; set; }
        [ForeignKey("UserId")]
        public virtual User Users { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Roles { get; set; }

    }
}
