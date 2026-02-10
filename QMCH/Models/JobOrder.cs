using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class JobOrder
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? Requirements { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }

        [StringLength(200)]
        public string? Location { get; set; }

        public int NumberOfPositions { get; set; } = 1;

        [StringLength(50)]
        public string Priority { get; set; } = "Medium";

        [StringLength(50)]
        public string Status { get; set; } = "Open";

        public decimal? PayRangeMin { get; set; }
        public decimal? PayRangeMax { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.Today;
        public DateTime? ClosedDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
