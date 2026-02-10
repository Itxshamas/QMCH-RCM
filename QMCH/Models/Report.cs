using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMCH.Models
{
    public class Report
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required, StringLength(50)]
        public string Category { get; set; } = string.Empty;

        // Alias for Category for ReportService
        [NotMapped]
        public string ReportType
        {
            get => Category;
            set => Category = value;
        }

        public string? GeneratedBy { get; set; }
        public DateTime GeneratedDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string Status { get; set; } = "Completed";

        public string? FileUrl { get; set; }

        public bool IsSystem { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
