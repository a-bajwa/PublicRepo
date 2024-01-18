using System.ComponentModel.DataAnnotations;

namespace BerryessaUnion.Domains.FileSetup
{
    public class FileToUpload
    {
        [Key]
        public long Id { get; set; }
        public string Storage { get; set; }
        public string FileName { get; set; }
        public string FileNameDownload { get; set; }
        public string Title { get; set; }
        public string FileType { get; set; }
        public string CharSet { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Tags { get; set; }
        public string Embed { get; set; }
        public Nullable<long> FileSize { get; set; }  
        public Nullable<long> Width { get; set; }
        public Nullable<long> Height { get; set; }
        public Nullable<long> Duration { get; set; }
        public Nullable<bool> IsPublic { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<long> DeletedBy { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
    }
}
