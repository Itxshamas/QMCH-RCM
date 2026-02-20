using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMCH.Models
{
    public class ServiceOrderSummary
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string ClientName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? MedicalRecordNumber { get; set; }

        public DateTime FromDate { get; set; } = DateTime.Today.AddDays(-30);
        public DateTime ToDate { get; set; } = DateTime.Today;

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalBilledAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalServiceHours { get; set; }

        public string? ServiceDetails { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }

        [NotMapped]
        public decimal AverageHourlyRate => TotalServiceHours > 0 ? TotalBilledAmount / TotalServiceHours : 0;
    }
}
