using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Domains.MenuSetup
{
    public class Menu
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public Nullable<long> ParentMenuId { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> MenuOrder { get; set; }
        public string MenuICon { get; set; }
        public string HeadTitle { get; set; }
        public string HeadTitle2 { get; set; }
        public string Type { get; set; }
        public string BadgeType { get; set; }
        public string BadgeValue { get; set; }
        public string BadgeClass { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Bookmark { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public Nullable<bool> IsMenu { get; set; }
    }
}
