using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharedLibrary.CommonFunctions.CommonEnums;

namespace BerryessaUnion.Domains.UserSetup
{
    public class UserAddres
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<long> DeletedBy { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        [ForeignKey("UserId")]
        public virtual User Users { get; set; }

    }
}
