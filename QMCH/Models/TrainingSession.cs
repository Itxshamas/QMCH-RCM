using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [StringLength(200)]
        public string? TrainerName { get; set; }

        [StringLength(200)]
        public string? Location { get; set; }

        public DateTime SessionDate { get; set; } = DateTime.Today;

        public int DurationHours { get; set; } = 2;
        public int MaxAttendees { get; set; } = 20;

        [StringLength(50)]
        public string Status { get; set; } = "Scheduled";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
