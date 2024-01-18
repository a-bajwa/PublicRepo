using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharedLibrary.CommonFunctions.CommonEnums;

namespace BerryessaUnion.Domains.MenuSetup
{
    public class MenusRight
    {
        [Key]
        public long Id { get; set; }
        public long MenuId { get; set; }
        public Nullable<RoleType> RoleType { get; set; }
        public Nullable<bool> IsView { get; set; }
        public Nullable<bool> IsCreate { get; set; }
        public Nullable<bool> IsUpdate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        [ForeignKey("MenuId")]
        public virtual Menu Menus { get; set; }
        
        public bool IsDeleted { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<long> DeletedBy { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
    }
}
