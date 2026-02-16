using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMCH.Models
{
    public class Visit
    {
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public int? StaffId { get; set; }
        public Staff? Staff { get; set; } = null!;

        [Required]
        public DateTime ScheduledStartTime { get; set; }

        [Required]
        public DateTime ScheduledEndTime { get; set; }

        public DateTime? ActualStartTime { get; set; }

        public DateTime? ActualEndTime { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Scheduled"; // Scheduled, In-Progress, Completed, Cancelled

        [StringLength(500)]
        public string? TaskDescription { get; set; }

        // Navigation Properties
        public ICollection<VisitDocumentation> Documentations { get; set; } = new List<VisitDocumentation>();

        // Audit Fields
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public class VisitDocumentation
    {
        [Key]
        public int Id { get; set; }

        public int VisitId { get; set; }
        public Visit Visit { get; set; } = null!;

        [Required, StringLength(100)]
        public string NoteType { get; set; } = "Clinical Note"; // SOAPNote, Assessment, Progress

        [Required]
        public string Content { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Observations { get; set; }

        // Vital Signs (stored as JSON)
        [StringLength(500)]
        public string? VitalSigns { get; set; } // {"temperature": 98.6, "blood_pressure": "120/80", "pulse": 72, "respiration": 16}

        // Medications Administered
        [StringLength(1000)]
        public string? MedicationsAdministered { get; set; }

        // Services Provided
        [StringLength(1000)]
        public string? ServicesProvided { get; set; }

        // Patient Condition & Response
        [StringLength(1000)]
        public string? PatientResponse { get; set; }

        // Clinical Notes
        [StringLength(2000)]
        public string? ClinicalNotes { get; set; }

        public bool IsSigned { get; set; }

        public DateTime? SignedAt { get; set; }

        public string? SignedBy { get; set; }

        // Digital Signature (Base64 encoded)
        public string? DigitalSignature { get; set; }

        // Audit Fields
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
