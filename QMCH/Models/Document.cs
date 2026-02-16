using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string FileName { get; set; } = string.Empty;

        [StringLength(300)]
        public string? OriginalFileName { get; set; }

        [StringLength(100)]
        public string? FileType { get; set; }

        public long? FileSize { get; set; }

        [StringLength(500)]
        public string? FilePath { get; set; }

        [StringLength(100)]
        public string? Category { get; set; }

        [StringLength(100)]
        public string? RelatedModule { get; set; }

        public int? RelatedEntityId { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(500)]
        public string? Tags { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }

        public bool IsConfidential { get; set; }

        [StringLength(200)]
        public string? UploadedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }

    public class DocumentRecord
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string EntityType { get; set; } = string.Empty; // Patient, Staff, Visit

        public int? EntityId { get; set; }

        [Required, StringLength(100)]
        public string DocumentType { get; set; } = string.Empty; // License, Certificate, Assessment, etc.

        [Required, StringLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required, StringLength(500)]
        public string FilePath { get; set; } = string.Empty;

        public long FileSize { get; set; }

        [StringLength(50)]
        public string FileExtension { get; set; } = string.Empty;

        public string? UploadedBy { get; set; }
        public User? UploadedByUser { get; set; }

        public DateTime UploadedDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        [StringLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

