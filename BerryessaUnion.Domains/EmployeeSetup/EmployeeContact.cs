using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerryessaUnion.Domains.EmployeeSetup
{
    public class EmployeeContact
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string? State { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State1 { get; set; }
        public string? Zip { get; set; }
        public string? MailingAddress { get; set; }
        public string? MailingCity { get; set; }
        public string? MailingState { get; set; }
        public string? MailingZip { get; set; }
        public string? Phone { get; set; }
        public string? WorkPhone { get; set; }
        public string? CellPhone { get; set; }
        public string? StateID { get; set; }
        public string? LocalID { get; set; }
        public int? Age { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Ethnicity { get; set; }
        public string? Race { get; set; }
        public string? Languages { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employees { get; set; }
    }
}
