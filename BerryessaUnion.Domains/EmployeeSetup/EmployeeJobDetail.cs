using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerryessaUnion.Domains.EmployeeSetup
{
    public class EmployeeJobDetail
    {
        [Key]
        public int ID { get; set; }
        public int? EmployeeID { get; set; }
        public string? JobGroup { get; set; }
        public string? BargainingGroup { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public string? Class { get; set; }
        public string? Department { get; set; }
        public string? PrimaryDepartment { get; set; }
        public int? QSSExtRef { get; set; }
        public decimal? DistrictExperience { get; set; }
        public decimal? OutofDistrictExperience { get; set; }
        public string? JobNumber { get; set; }
        public string? JobType { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public string? Description { get; set; }
        public string? Notes1 { get; set; }
        public string? PrimaryJob { get; set; }
        public string? JobClassification { get; set; }
        public string? JobClassificationAbbrev { get; set; }
        public string? JobGroup1 { get; set; }
        public string? BargainingGroup1 { get; set; }
        public string? Class1 { get; set; }
        public string? Range { get; set; }
        public Nullable<DateTime> SeniorityDate { get; set; }
        public string? SeniorityNumber { get; set; }
        public Nullable<DateTime> LongevityDate { get; set; }
        public Nullable<DateTime> TenureDate { get; set; }
        public string? SeniorityUnits { get; set; }
        public string? SeniorityNotes { get; set; }
        public Nullable<DateTime> VacationBaseDate { get; set; }
        public string? CurrentStep { get; set; }
        public Nullable<DateTime> CurrentStepDate { get; set; }
        public string? CurrentLevel { get; set; }
        public Nullable<DateTime> CurrentLevelDate { get; set; }
        public string? CurrentLongevity { get; set; }
        public Nullable<DateTime> CurrentLongevityDate { get; set; }
        public string? CurrentStatus { get; set; }
        public Nullable<DateTime> CurrentStatusDate { get; set; }
        public string? CurrentStatusFTE { get; set; }
        public string? YearStep { get; set; }
        public Nullable<DateTime> YearStepDate { get; set; }
        public string? YearLevel { get; set; }
        public Nullable<DateTime> YearLevelDate { get; set; }
        public string? YearLongevity { get; set; }
        public Nullable<DateTime> YearLongevityDate { get; set; }
        public string? YearStatus { get; set; }
        public Nullable<DateTime> YearStatusDate { get; set; }
        public string? ServiceYears { get; set; }
        public Nullable<DateTime> EffectiveDate { get; set; }
        public string? PositionID { get; set; }
        public string? Definition { get; set; }
        public string? ExtraDescription { get; set; }
        public string? State2 { get; set; }
        public string? Type1 { get; set; }
        public string? JobClassification1 { get; set; }
        public string? JobClassificationAbbrev1 { get; set; }
        public string? JobCode { get; set; }
        public string? Family { get; set; }
        public string? Department1 { get; set; }
        public string? Schedule { get; set; }
        public string? Function { get; set; }
        public string? Subject { get; set; }
        public string? Track { get; set; }
        public string? Manager { get; set; }
        public string? Supervisor { get; set; }
        public string? BargainingGroup2 { get; set; }
        public Nullable<DateTime> StartDate1 { get; set; }
        public Nullable<DateTime> EndDate1 { get; set; }
        public string? HomeRoom { get; set; }
        public string? Notes2 { get; set; }
        public string? AnniversaryDate { get; set; }
        public string? Notes { get; set; }
        // Foreign key navigation property
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
    }
}


